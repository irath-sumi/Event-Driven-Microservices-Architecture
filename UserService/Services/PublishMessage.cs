using RabbitMQ.Client;
using System.Text;

namespace UserService.Services
{
    public static class PublishMessage
    {
        /// <summary>
        /// Publishes message to the message queue using RabbitMQ, a popular open source message broker.
        /// </summary>
        /// <param name="integrationEvent"></param>
        /// <param name="eventData"></param>
        public static void PublishMessageToQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();

            // creates connection to the RabbitMQ server.
            using (var connection = factory.CreateConnection())
            {
                // creates communication channel to the RabbitMQ server
                using (var channel = connection.CreateModel())
                {
                    // converts the data to be published to byte aray
                    var body = Encoding.UTF8.GetBytes(eventData);

                    // publishes the message
                    channel.BasicPublish(exchange: "user", routingKey: integrationEvent, basicProperties: null, body: body);
                }
            }


        }
    }
}
