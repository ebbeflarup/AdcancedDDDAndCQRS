namespace Restaurant.Messages.Commands
{
    public class SendToMeIn
    {
        public int Seconds { get; }
        public IMessage InnerMessage { get; }

        public SendToMeIn(int seconds, IMessage message)
        {
            Seconds = seconds;
            InnerMessage = message;
        }
    }
}
