using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProducerApi.MessageBrokers
{
    public class RabbitMQProducer : IMessageProducer
    {

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue:"students",
                                  exclusive:false,
                                  autoDelete:false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "students", body: body);
        }
    }
}
