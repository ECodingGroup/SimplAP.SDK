namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class BoundingPolyDto
    {
        public IEnumerable<VertexDto> Vertices { get; set; }
        public IEnumerable<NormalizedVertexDto> NormalizedVertices { get; set; }
    }
}
