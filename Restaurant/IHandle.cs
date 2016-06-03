using Restaurant.Messages;

namespace Restaurant
{
    public interface IHandle
    { }

    public interface IHandle<in T> :IHandle
    {
        void Handle(T t);
    }
}
