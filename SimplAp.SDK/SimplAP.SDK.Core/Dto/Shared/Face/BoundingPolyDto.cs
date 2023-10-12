using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto.Shared.Face
{
    public class BoundingPolyDto
    {
        public IEnumerable<VertexDto> Vertices { get; set; }
        public IEnumerable<NormalizedVertexDto> NormalizedVertices { get; set; }
    }
}
