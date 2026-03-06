using Xunit;
using EchoGridLibrary.Puzzle;
using EchoGridLibrary.Events;
using System.Collections.Generic;

namespace EchoGridLibrary.Tests
{
    public class GameLogicTests
    {
        private DoorLogic _door;

        public GameLogicTests()
        {
            _door = new DoorLogic();
            _door.RequiredSwitchIds = new List<int> { 1, 2 };
        }

        [Fact]
        public void Door_Does_Not_Open_With_Single_Switch()
        {
            _door.HandleSwitchActivated(1);
            Assert.False(_door.IsOpen);
        }

        [Fact]
        public void Door_Opens_With_All_Switches()
        {
            _door.HandleSwitchActivated(1);
            _door.HandleSwitchActivated(2);
            Assert.True(_door.IsOpen);
        }

        [Fact]
        public void Door_Closes_When_Switch_Lost()
        {
            _door.HandleSwitchActivated(1);
            _door.HandleSwitchActivated(2);
            _door.HandleSwitchDeactivated(1);
            Assert.False(_door.IsOpen);
        }

        [Fact]
        public void EventBus_Relays_Data_Correctly()
        {
            int receivedId = -1;
            EventBus.OnSwitchActivated += (id) => receivedId = id;
            
            EventBus.TriggerSwitchActivated(99);
            
            Assert.Equal(99, receivedId);
        }
    }
}
