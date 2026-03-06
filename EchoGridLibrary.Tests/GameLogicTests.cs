using Xunit;
using EchoGridLibrary.Puzzle;
using EchoGridLibrary.Events;
using EchoGridLibrary.Core;
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
            int activatedId = -1;
            int deactivatedId = -1;
            List<FrameInput>? recordedData = null;

            EventBus.OnSwitchActivated += (id) => activatedId = id;
            EventBus.OnSwitchDeactivated += (id) => deactivatedId = id;
            EventBus.OnEchoRecorded += (data) => recordedData = data;
            
            EventBus.TriggerSwitchActivated(99);
            EventBus.TriggerSwitchDeactivated(88);
            var testData = new List<FrameInput> { new FrameInput { timestamp = 1.0f } };
            EventBus.TriggerEchoRecorded(testData);
            
            Assert.Equal(99, activatedId);
            Assert.Equal(88, deactivatedId);
            Assert.NotNull(recordedData);
            Assert.Single(recordedData);
        }

        [Fact]
        public void EchoGrid_Basic_Math_Operations()
        {
            var calc = new EchoGrid();
            Assert.Equal(10, calc.Add(7, 3));
            Assert.Equal(4, calc.Subtract(7, 3));
            Assert.Equal(21, calc.Multiply(7, 3));
            Assert.Equal(2, calc.Divide(6, 3));
            Assert.Throws<DivideByZeroException>(() => calc.Divide(5, 0));
        }

        [Fact]
        public void MathStructs_Constructor_Tests()
        {
            var v3 = new Vector3(1, 2, 3);
            Assert.Equal(1f, v3.x);
            Assert.Equal(2f, v3.y);
            Assert.Equal(3f, v3.z);

            var q = new Quaternion(0, 0, 0, 1);
            Assert.Equal(0f, q.x);
            Assert.Equal(0f, q.z);
            Assert.Equal(1f, q.w);
        }
    }
}
