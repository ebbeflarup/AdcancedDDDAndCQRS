using System.Threading;

namespace Messages.Commands
{
    public class SendToMeIn<TMessage>
    {
        private readonly int _seconds;
        private readonly TMessage _message;

        public SendToMeIn(int seconds, TMessage message)
        {
            _seconds = seconds;
            _message = message;
        }
    }
}
