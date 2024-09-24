using MediatR;

namespace Kafka.Models
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; set; }
        
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
