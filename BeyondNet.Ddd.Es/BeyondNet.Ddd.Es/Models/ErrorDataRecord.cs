namespace BeyondNet.Ddd.Es.Models
{
    public class ErrorDataRecord
    {
        public Guid Id { get; set; } = default!;
        public string AggregateId { get; set; } = default!;
        public string ErrorMessage { get; set; } = default!;
        public string? StackTrace { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
    }
}
