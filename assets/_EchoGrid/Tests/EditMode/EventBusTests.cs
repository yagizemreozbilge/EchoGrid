using NUnit.Framework;
using EchoGrid.Events;

namespace EchoGrid.Tests
{
    // These tests ensure our central nervous system (EventBus) is routing data correctly without loss!
    public class EventBusTests
    {
        private bool _wasCalled;
        private int _receivedId;

        [SetUp]
        public void Setup()
        {
            _wasCalled = false;
            _receivedId = -1;
        }

        private void DummySwitchActivatedHandler(int switchId)
        {
            _wasCalled = true;
            _receivedId = switchId;
        }

        [Test]
        public void EventBus_Fires_Switch_Activated_Event_To_Subscribers()
        {
            // Arrange
            EventBus.OnSwitchActivated += DummySwitchActivatedHandler;

            // Act: Trigger an ID
            EventBus.TriggerSwitchActivated(42);

            // Assert
            Assert.IsTrue(_wasCalled);
            Assert.AreEqual(42, _receivedId);

            // Cleanup
            EventBus.OnSwitchActivated -= DummySwitchActivatedHandler;
        }
    }
}
