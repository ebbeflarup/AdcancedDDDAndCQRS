namespace Messages.Commands
{
    public class RemoveFromTenSecondWindow
    {
        public int StopLossCandidate { get; private set; }
        public RemoveFromTenSecondWindow(int stopLossCandidate)
        {
            StopLossCandidate = stopLossCandidate;
        }
    }
}
