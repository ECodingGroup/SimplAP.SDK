using Newtonsoft.Json;
using SimplAP.SDK.Core.Dto;
using SimplAP.SDK.Core.Enums;
using SimplAP.SDK.Core.Services;
using SimplAP.SDK.NET.Tester;

var authService = new SimplAPAuthService("", "", "", "");
var accessToken = await authService.GetAccessTokenAsync();

var aiService = new SimplAPService();

Console.WriteLine("Provide an image path on which to perform AI");
var filePath = Console.ReadLine();
var extension = Path.GetExtension(filePath);
while (!File.Exists(filePath) || string.IsNullOrEmpty(extension) || (!extension.EndsWith(".pdf") && !extension.EndsWith(".jpg") && !extension.EndsWith(".png") && !extension.EndsWith(".jpeg")))
{
    Console.WriteLine("Provide an image path on which to perform AI");
    filePath = Console.ReadLine();
}

var fileBytes = File.ReadAllBytes(filePath);

var input = new ProcessingExtendedInput(AIModelType.IdCard, extension.ToLower().EndsWith(".pdf") ? ProcessedImageType.PDF : ProcessedImageType.Image, fileBytes, ImageAIProcessingType.SignatureDetection)
{
    ReturnPdfFileBytesOnOutput = false
};

var result = await aiService.ProcessAIAsync(input, accessToken);

Console.WriteLine(JsonConvert.SerializeObject(result));

//if(result.Result == null || result.Result.All(x => x.Entities == null))
//{ return; }


//var entities = result.Result
//    .Where(x => x.Entities != null)
//    .FirstOrDefault();

//var pdfImage = entities.PDFImage; 

//var firstDetectedEntity = entities
//    .Entities
//    .Where(x => x.DetectedObject.WasProcessingSuccessful)
//    .FirstOrDefault();

//if(firstDetectedEntity == default || firstDetectedEntity.DetectedObject == null) 
//{ return; }

//var coordinates = firstDetectedEntity.DetectedObject.Result.BBox;
//var width = (int)(coordinates.Xmax - coordinates.Xmin);
//var height = (int)(coordinates.Ymax - coordinates.Ymin);
//var croppedImage = ImageHelpers.CropImage(pdfImage ?? fileBytes, (int)coordinates.Xmin, (int)coordinates.Ymin, width, height, 5);

//if (firstDetectedEntity.RollAngle.Result != null && firstDetectedEntity.RollAngle.Result != 0)
//{
//    croppedImage = ImageHelpers.RotateImage(croppedImage, (float)firstDetectedEntity.RollAngle.Result);
//}

//extension = extension == ".pdf" ? ".jpg" : extension;
//File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}_cropped{extension}"), croppedImage);

