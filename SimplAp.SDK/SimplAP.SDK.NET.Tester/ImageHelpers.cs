using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
namespace SimplAP.SDK.NET.Tester;

public static class ImageHelpers
{
    /// <summary>
    /// Resize the uploaded image only if the new width is smaller than the original, get it using the fully specified path and new width on the input,
    /// calculate the new dimensions while keeping the aspect ratio and finally save the changed image directly as a file on the file system.
    /// </summary>
    /// <param name="newWidth"></param>
    /// <param name="stPhotoPath"></param>
    public static void DownscaleImage(int newWidth, string stPhotoPath)
    {
        if (IsImageFile(stPhotoPath))
        {
            try
            {
                var imgPhoto = Image.Load(stPhotoPath);

                if (newWidth < imgPhoto.Width)
                {
                    //keep aspect ratio

                    var heightRatio = (double)imgPhoto.Height / (double)imgPhoto.Width;

                    var newHeight = (int)((double)newWidth * heightRatio);

                    imgPhoto = ResizeImage(imgPhoto, newWidth, newHeight);

                    imgPhoto.Save(stPhotoPath);
                }
            }
            catch
            {
                //ignore
            }
        }
    }

    public static Image ResizeImage(Image image, int width, int height)
    {
        image.Mutate(x => x.Resize(width, height));
        return image;
    }

    public static bool IsImageFile(string nameOrPath)
    {
        var ext = Path.GetExtension(nameOrPath);
        if (ext.StartsWith("."))
        {
            ext = ext.TrimStart('.');
        }

        ext = ext.ToLower();
        if (ImageExtensions.Any(x => x == ext))
        {
            return true;
        }

        return false;
    }

    public static byte[] CropImage(byte[] imageInput, int x, int y, int width, int height, int percentagePadding)
    {
        var img = Crop(Image.Load<Rgba32>(imageInput), width, height, x, y, percentagePadding);
        return ImageToByteArray(img);
    }

    private static Image<TPixel> Crop<TPixel>(Image<TPixel> image, int width, int height, int x, int y, int? percentagePadding)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        try
        {
            int oWidth = width;
            int oHeight = height;
            int oX = x;
            int oY = y;

            //try to add percentagePadding px for each direction, if possible
            int xPercentage = (int)(image.Width * ((float)percentagePadding / 100f));
            int yPercentage = (int)(image.Height * ((float)percentagePadding / 100f));

            x -= xPercentage;
            if (x < 0)
            {
                x = 0;
            }
            y -= yPercentage;
            if (y < 0)
            {
                y = 0;
            }

            width = (width + (oX - x)) + xPercentage;
            if (width + x > image.Width)
            { width = image.Width - x; }
            height = (height + (oY - y)) + yPercentage;
            if (height + y > image.Height)
            { height = image.Height - y; }

            // Define a rectangle section of the image
            var rect = new SixLabors.ImageSharp.Rectangle(x, y, width, height);

            // Create the cropped image
            Image<TPixel> cropped = image.Clone(ctx => ctx.Crop(rect));

            return cropped;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static byte[] RotateImage(byte[] image, float rotateAngle)
    {
        using var ms = new MemoryStream(image);
        using var img = Image.Load<Rgba32>(ms);
        var rotated = RotateImage(img, rotateAngle);
        using var msoutput = new MemoryStream();
        rotated.Save(msoutput, new JpegEncoder());
        return msoutput.ToArray();
    }

    private static Image<TPixel> RotateImage<TPixel>(Image<TPixel> img, float rotateAngle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        // Calculate the size of the new canvas necessary to hold the rotated image
        var rotatedWidth = (int)(img.Width * Math.Abs(Math.Cos(rotateAngle)) + img.Height * Math.Abs(Math.Sin(rotateAngle)));
        var rotatedHeight = (int)(img.Height * Math.Abs(Math.Cos(rotateAngle)) + img.Width * Math.Abs(Math.Sin(rotateAngle)));

        // Create a new image with the calculated dimensions
        Image<TPixel> returnImage = new Image<TPixel>(rotatedWidth, rotatedHeight);

        // Rotate the original image
        img.Mutate(x => x.Rotate(rotateAngle));

        // Find the position to draw the rotated image on the canvas
        var posX = (rotatedWidth - img.Width) / 2;
        var posY = (rotatedHeight - img.Height) / 2;

        // Draw the rotated image on the new canvas, centered
        returnImage.Mutate(x => x.DrawImage(img, new SixLabors.ImageSharp.Point(posX, posY), 1f));

        return returnImage;
    }

    public static List<byte[]> ConvertPDFtoImage(byte[] inputPdf, int dpi, string pathToDLL, string pdfTempPath)
    {
        var tempGuid = Guid.NewGuid();
        var tempFileName = $"{tempGuid}.pdf";
        tempFileName = Path.Combine(pdfTempPath, tempFileName);
        var outputFileName = $"{tempGuid}-%d.png";
        outputFileName = Path.Combine(pdfTempPath, outputFileName);

        if (!Directory.Exists(pdfTempPath))
        {
            Directory.CreateDirectory(pdfTempPath);
        }

        File.WriteAllBytes(tempFileName, inputPdf);

        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = pathToDLL,  // path to Ghostscript executable
                Arguments = $"-dSAFER -dBATCH -dNOPAUSE -sDEVICE=png16m -r{dpi} -sOutputFile={outputFileName} {tempFileName}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                RedirectStandardError = true, // This is the key line
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();
            var error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            process.WaitForExit();
            var pngFiles = Directory.GetFiles(Path.Combine(pdfTempPath), $"{tempGuid}-*.png");

            List<byte[]> images = pngFiles.Select(File.ReadAllBytes).ToList();

            foreach (var imageFile in pngFiles)
            {
                try
                {
                    File.Delete(imageFile);
                }
                catch (Exception ex)
                {
                    throw;
                    //ignore
                }
            }
            return images;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            try
            {
                File.Delete(tempFileName);
            }
            catch (Exception ex)
            {
                //ignore
            }
        }
    }

    public static Image<TPixel> NormalizeImage<TPixel>(Image<TPixel> image)
       where TPixel : unmanaged, IPixel<TPixel>
    {
        if (image.Metadata.ExifProfile != null)
        {
            if (image.Metadata.ExifProfile.TryGetValue(ExifTag.Orientation, out var orientationTag) && orientationTag != null)
            {
                var orientation = orientationTag.Value;

                if (orientation == 6)
                {
                    image.Mutate(x => x.Rotate(RotateMode.Rotate90));
                }
                else if (orientation == 8)
                {
                    image.Mutate(x => x.Rotate(RotateMode.Rotate270));
                }
                else if (orientation == 3)
                {
                    image.Mutate(x => x.Rotate(RotateMode.Rotate180));
                }
            }
        }
        return image;
    }

    private static string[] ImageExtensions = new string[] { "bmp", "gif", "jpeg", "jpg", "png", "tif", "tiff" };

    private static byte[] ImageToByteArray(Image<Rgba32> image)
    {
        using var memoryStream = new MemoryStream();
        image.Save(memoryStream, new JpegEncoder());
        return memoryStream.ToArray();
    }

}