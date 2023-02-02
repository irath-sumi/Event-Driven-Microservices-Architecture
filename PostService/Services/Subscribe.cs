using Newtonsoft.Json.Linq;
using PostService.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PostService.Services
{
    public static class Subscribe
    {
        public static void ListenForIntegrationEvents(PostServiceContext dbcontext)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            // event fired when delivery arrives for the consumer. When the "Received" event is raised by the "consumer" object,
            // the delegate will be triggered and will run the code that is specified in its body
            consumer.Received += (sender, args) =>
            {
                //var contextOptions = new DbContextOptionsBuilder<PostServiceContext>()
                //    .UseSqlServer(_configuration.GetConnectionString("LocalDbConnection"))
                //    .Options;
                //var dbContext = new PostServiceContext(contextOptions);

                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                var data = JObject.Parse(message);
                var type = args.RoutingKey;

                if (type == "user.add")
                {
                    dbcontext.Users.Add(new Entities.User
                    {
                        ID = data["id"].Value<int>(),
                        Name = data["name"].Value<string>()
                    });
                    dbcontext.SaveChanges();
                }
                else if (type == "user.update")
                {
                    Entities.User? user = dbcontext.Users.FirstOrDefault(x => x.ID == data["id"].Value<int>());
                    user.Name = data["newname"].Value<string>();
                    dbcontext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: "user.postservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
    }
}
