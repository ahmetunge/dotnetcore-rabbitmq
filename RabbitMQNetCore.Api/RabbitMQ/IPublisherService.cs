using System.Collections.Generic;
using RabbitMQNetCore.Api.Helpers;

namespace RabbitMQNetCore.Api.RabbitMQ
{
    public interface IPublisherService
    {
        IResult AddQueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new();
    }
}