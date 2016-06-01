using System.Linq;
using AdvancedCourse;
using Messages.Commands;
using Messages.Events;
using Xunit;

namespace UnitTests
{
    public class ProcessManagerTests
    {
        private readonly FakeMessagePublisher _fakeMessagePublisher;
        private readonly StopLossProcessManager _stopLossProcessManager;

        public ProcessManagerTests()
        {
            _fakeMessagePublisher = new FakeMessagePublisher();
            _stopLossProcessManager = new StopLossProcessManager(_fakeMessagePublisher);
        }

        [Fact]
        public void StopLossLimitIsCorrect()
        {
            _stopLossProcessManager.Handle(new PositionAcquired(100));

            Assert.Equal(90, _stopLossProcessManager.StopLossLimit);
        }

        [Fact]
        public void ShouldPublishSendToMeInTenSeconds()
        {
            _stopLossProcessManager.Handle(new PriceUpdated(100));

            Assert.Equal(1, _fakeMessagePublisher.PublishedMessages.Count);

            var publishedMessage = _fakeMessagePublisher.PublishedMessages.First();

            Assert.IsType<SendToMeIn<RemoveFromTenSecondWindow>>(publishedMessage);
        }

        [Fact]
        public void ShouldUpdateStopLossAfterTenSecondWindow()
        {
            _stopLossProcessManager.Handle(new PositionAcquired(100));
            _stopLossProcessManager.Handle(new PriceUpdated(120));
            _stopLossProcessManager.Handle(new RemoveFromTenSecondWindow(120));

            var publishedMessage = _fakeMessagePublisher.PublishedMessages.Last();

            Assert.IsType<StopLossPriceUpdated>(publishedMessage);
        }

        [Fact]
        public void ShouldUpdateStopLossToNewPrice()
        {
            _stopLossProcessManager.Handle(new PositionAcquired(100));
            _stopLossProcessManager.Handle(new PriceUpdated(120));
            _stopLossProcessManager.Handle(new RemoveFromTenSecondWindow(120));

            var stopLossUpdated = _fakeMessagePublisher.PublishedMessages.Last() as StopLossPriceUpdated;

            Assert.Equal(110, stopLossUpdated.NewStopLossLimit);
        }

        [Fact]
        public void ShouldHitStopLossAfterThirteenSecondWindow()
        {
            _stopLossProcessManager.Handle(new PositionAcquired(100));
            _stopLossProcessManager.Handle(new PriceUpdated(80));
            _stopLossProcessManager.Handle(new RemoveFromThirteenSecondWindow(80));

            var publishedMessage = _fakeMessagePublisher.PublishedMessages.Last();

            Assert.IsType<StopLossHit>(publishedMessage);
        }
    }
}
