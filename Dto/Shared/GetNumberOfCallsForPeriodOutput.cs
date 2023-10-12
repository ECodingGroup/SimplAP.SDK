namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class GetNumberOfCallsForPeriodOutput
    {
        public int TotalNumberOfCalls { get; set; }
        public DateTimeOffset CallsFrom { get; set; }
        public DateTimeOffset CallsUntil { get; set; }
        public IEnumerable<NumberOfCallsPerProductDto> CallsByProduct { get; set; }
    }
}
