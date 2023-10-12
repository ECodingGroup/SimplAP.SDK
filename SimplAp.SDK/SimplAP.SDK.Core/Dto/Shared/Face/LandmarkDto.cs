using SimplAP.SDK.Core.Enums;

namespace SimplAP.SDK.Core.Dto.Shared.Face
{
    public sealed class LandmarkDto
    {
        public LandmarkType Type { get; set; }
        public PositionDto Position { get; set; }
    }
}
