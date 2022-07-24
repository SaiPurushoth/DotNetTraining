namespace ProducerApi.MessageBrokers
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
