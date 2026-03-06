using NUnit.Framework;
using EchoGridLibrary.Puzzle;
using EchoGridLibrary.Events;
using System.Collections.Generic;

namespace EchoGridLibrary.Tests
{
    [TestFixture]
    public class GameLogicTests
    {
        private DoorLogic _door;

        [SetUp]
        public void Setup()
        {
            _door = new DoorLogic();
            _door.RequiredSwitchIds = new List<int> { 1, 2 };
        }

        [Test]
        public void Door_Does_Not_Open_With_Single_Switch()
        {
            _door.HandleSwitchActivated(1);
            Assert.IsFalse(_door.IsOpen);
        }

        [Test]
        public void Door_Opens_With_All_Switches()
        {
            _door.HandleSwitchActivated(1);
            _door.HandleSwitchActivated(2);
            Assert.IsTrue(_door.IsOpen);
        }

        [Test]
        public void Door_Closes_When_Switch_Lost()
        {
            _door.HandleSwitchActivated(1);
            _door.HandleSwitchActivated(2);
            _door.HandleSwitchDeactivated(1);
            Assert.IsFalse(_door.IsOpen);
        }

        [Test]
        public void EventBus_Relays_Data_Correctly()
        {
            int receivedId = -1;
            EventBus.OnSwitchActivated += (id) => receivedId = id;
            
            EventBus.TriggerSwitchActivated(99);
            
            Assert.AreEqual(99, receivedId);
        }
    }
}
