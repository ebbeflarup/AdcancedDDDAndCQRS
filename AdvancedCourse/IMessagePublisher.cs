namespace AdvancedCourse
{
    public interface IMessagePublisher
    {
        void Publish<TMessage>(TMessage message);
    }
}