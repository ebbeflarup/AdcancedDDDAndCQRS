namespace Restaurant
{
    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message);
    }
}