namespace Restaurant
{
    public interface IMonitorable
    {
        string Name { get; }
        int Count();
    }
}