namespace Restaurant
{
    public interface IHandle<in T>
    {
        void Handle(T t);
    }
}
