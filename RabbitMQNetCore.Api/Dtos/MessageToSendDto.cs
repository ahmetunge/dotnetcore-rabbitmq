using System;

namespace RabbitMQNetCore.Api.Dtos
{
    public class MessageToSendDto
    {
        public MessageToSendDto()
        {
            ReceiverId = Guid.NewGuid();
            SenderId = Guid.NewGuid();
        }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid ReceiverId { get; private set; }
        public Guid SenderId { get; private set; }
    }
}