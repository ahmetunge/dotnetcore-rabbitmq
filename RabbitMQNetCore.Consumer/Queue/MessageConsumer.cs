using System;

namespace RabbitMQNetCore.Consumer.Queue
{
    public class MessageConsumer
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid ReceiverId { get;  set; }
        public Guid SenderId { get;  set; }
    }
}