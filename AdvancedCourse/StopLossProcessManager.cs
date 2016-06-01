using System.Collections.Generic;
using System.Linq;
using Messages.Commands;
using Messages.Events;


namespace AdvancedCourse
{
    public class StopLossProcessManager
    {
        private readonly IMessagePublisher _messagePublisher;
        public int StopLossLimit { get; private set; }
        private readonly List<int> _tenSecondWindow  = new List<int>(); 
        private readonly List<int> _thirteenSecondWindow = new List<int>(); 

        public StopLossProcessManager(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public void Handle(PriceUpdated priceUpdated)
        {
            var newStopLossLimitCandidate = priceUpdated.Price - 10;
            _tenSecondWindow.Add(newStopLossLimitCandidate);
            _thirteenSecondWindow.Add(priceUpdated.Price);

            _messagePublisher.Publish(new SendToMeIn<RemoveFromTenSecondWindow>(10, new RemoveFromTenSecondWindow(newStopLossLimitCandidate)));
            _messagePublisher.Publish(new SendToMeIn<RemoveFromThirteenSecondWindow>(13, new RemoveFromThirteenSecondWindow(priceUpdated.Price)));
        }

        public void Handle(PositionAcquired positionAcquired)
        {
            StopLossLimit = positionAcquired.Price - 10;
        }

        public void Handle(RemoveFromTenSecondWindow removeFromTenSecondWindow)
        {
            var minPriceTenSecondWindow = _tenSecondWindow.Min();
            if (minPriceTenSecondWindow > StopLossLimit)
            {
                StopLossLimit = minPriceTenSecondWindow;
                _messagePublisher.Publish(new StopLossPriceUpdated(StopLossLimit));
            }
            
            _tenSecondWindow.Remove(removeFromTenSecondWindow.Price);
        }

        public void Handle(RemoveFromThirteenSecondWindow removeFromTenSecondWindow)
        {
            var maxPriceThirteenSecondWindow = _thirteenSecondWindow.Max();
            if (maxPriceThirteenSecondWindow < StopLossLimit)
            {
                _messagePublisher.Publish(new StopLossHit());
            }

            _thirteenSecondWindow.Remove(removeFromTenSecondWindow.Price);
        }
    }
}
