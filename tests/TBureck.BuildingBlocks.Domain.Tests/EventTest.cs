using Xunit;

namespace TBureck.BuildingBlocks.Domain
{
    
    public class EventTest
    {

        [Fact]
        public void Raise_InvokesEvent()
        {
            int raisedEventCount = 0;
            Event<DummyEvent>.Raised += _ => raisedEventCount++;

            Event<DummyEvent>.Raise(new DummyEvent());
            
            Assert.Equal(1, raisedEventCount);
        }

        private class DummyEvent { }
    }
}