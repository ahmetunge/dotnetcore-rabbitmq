using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQNetCore.Api.Helpers;

namespace RabbitMQNetCore.Api.RabbitMQ
{
    public class PublisherService : IPublisherService
    {
        public IConfiguration Configuration { get; }
        RabbitMQConfiguration _rabbitMQConfiguration;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(IConfiguration configuration, ILogger<PublisherService> logger)
        {
            Configuration = configuration;
            _rabbitMQConfiguration = Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
            _logger = logger;
        }
        public IResult AddQueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMQConfiguration.HostName,
                    UserName = _rabbitMQConfiguration.UserName,
                    Password = _rabbitMQConfiguration.Password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Message",
                                                    durable: false,
                                                    exclusive: false,
                                                    autoDelete: false,
                                                    arguments: null);

                    foreach (var queueDataModel in queueDataModels)
                    {
                        string strBody = JsonConvert.SerializeObject(queueDataModel);
                        var body = Encoding.UTF8.GetBytes(strBody);
                        channel.BasicPublish(exchange: "",
                                             routingKey: queueName,
                                             body: body);
                        _logger.LogInformation($"{strBody} Message added queue");
                    }
                    return new Result(true, "Message added queue");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Message could not be added to the queue");
                return new Result(false, "Message could not be added to the queue");
            }
        }
    }
}