using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public sealed class LandmarkDto
    {
        public LandmarkType Type { get; set; }
        public PositionDto Position { get; set; }
    }
}
