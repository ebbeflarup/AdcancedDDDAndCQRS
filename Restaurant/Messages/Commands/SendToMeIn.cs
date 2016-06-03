namespace Restaurant.Messages.Commands
{
    public class SendToMeIn<TMessage>
    {
        public int Seconds { get; }
        public TMessage InnerMessage { get; }

        public SendToMeIn(int seconds, TMessage message)
        {
            Seconds = seconds;
            InnerMessage = message;
        }
    }
}
