namespace Messages.Events
{
    public class StopLossPriceUpdated
    {
        public int NewStopLossLimit { get; private set; }

        public StopLossPriceUpdated(int newStopLossLimit)
        {
            NewStopLossLimit = newStopLossLimit;
        }
    }
}
