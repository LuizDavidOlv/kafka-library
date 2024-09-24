namespace Kafka.Models
{
    public class ProduceResult
    {
        public Status Status { get; }
        public IEnumerable<string> Messages { get; }

        public ProduceResult(Status status, IEnumerable<string> messages = null)
        {
            Status = status;
            Messages = messages ?? Enumerable.Empty<string>();
        }
        

        public static readonly ProduceResult Pass = new ProduceResult(Status.Pass, new[] { "Message sent." });
    }

    public enum Status
    {
        Fail,
        Pass
    }
}
