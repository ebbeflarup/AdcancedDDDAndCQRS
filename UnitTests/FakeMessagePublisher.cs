using System.Collections.Generic;
using AdvancedCourse;

namespace UnitTests
{
    public class FakeMessagePublisher : IMessagePublisher
    {
        public IList<object> PublishedMessages = new List<object>();

        public void Publish<TMessage>(TMessage message)
        {
            PublishedMessages.Add(message);
        }
    }
}
