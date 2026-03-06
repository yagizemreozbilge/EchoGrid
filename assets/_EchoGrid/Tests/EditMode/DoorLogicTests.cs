using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using EchoGrid.Puzzle;
using EchoGrid.Events;

namespace EchoGrid.Tests
{
    public class DoorLogicTests
    {
        private GameObject _doorObject;
        private Door _door;

        [SetUp]
        public void Setup()
        {
            // Build the GameObject and attach the script dynamically exactly like Unity does
            _doorObject = new GameObject("TestDoor");
            _door = _doorObject.AddComponent<Door>();

            // Let's inject a requirement: This door requires Switch 1 and Switch 2 to be active to open
            // (Using reflection to set the private private field for testing)
            var fieldInfo = typeof(Door).GetField("requiredSwitchIds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(_door, new List<int> { 1, 2 });

            // Force OnEnable to trigger event subscriptions
            _door.SendMessage("OnEnable");
        }

        [TearDown]
        public void TearDown()
        {
            _door.SendMessage("OnDisable");
            Object.DestroyImmediate(_doorObject);
        }

        [Test]
        public void Door_Does_Not_Open_With_Partial_Switches_Active()
        {
            // Act: We only trigger one of the required switches
            EventBus.TriggerSwitchActivated(1);

            // Assert: The door must still be closed (IsOpen = false)
            Assert.IsFalse(_door.IsOpen, "The door should be closed because Switch 2 is not active yet.");
        }

        [Test]
        public void Door_Opens_When_All_Required_Switches_Are_Active()
        {
            // Act: Simulate player or Echo Clone pressing both required switches
            EventBus.TriggerSwitchActivated(1);
            EventBus.TriggerSwitchActivated(2);

            // Assert: The door logic should correctly recognize it's fully powered
            Assert.IsTrue(_door.IsOpen, "The door should be open because both required switches are active!");
        }

        [Test]
        public void Door_Closes_When_A_Switch_Is_Deactivated()
        {
            // Act
            EventBus.TriggerSwitchActivated(1);
            EventBus.TriggerSwitchActivated(2);
            // Verify it opened first
            Assert.IsTrue(_door.IsOpen);

            // Now player steps off Switch 1 (Or clone vanishes)
            EventBus.TriggerSwitchDeactivated(1);

            // Assert: The Door should close instantly to lock the path again
            Assert.IsFalse(_door.IsOpen, "The door should be closed after Switch 1 lost power.");
        }
    }
}
