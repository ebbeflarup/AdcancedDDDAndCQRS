namespace Restaurant
{
    public interface IPublisher
    {
        void Publish(string topic, Order order);
    }
}