using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitMQNetCore.Api.Dtos;
using RabbitMQNetCore.Api.Helpers;
using RabbitMQNetCore.Api.RabbitMQ;

namespace RabbitMQNetCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public MessagesController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpPost("send")]
        public IActionResult Send(MessageToSendDto messageToSend)
        {
            List<MessageToSendDto> messages = new List<MessageToSendDto>();
            messages.Add(messageToSend);

            IResult result = _publisherService.AddQueue(messages, "Message");
            if (result.Success)
            {
                return Ok(result.Message);

            }
            return BadRequest(result.Message);
        }
    }
}