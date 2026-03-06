using System;
using System.Collections.Generic;
using EchoGrid.EchoSystem;

namespace EchoGrid.Events
{
    // A centralized hub to loosely couple our game systems.
    // Switches don't need to know about doors, and doors don't need to know about switches directly.
    public static class EventBus
    {
        public static event Action<int> OnSwitchActivated;
        public static event Action<int> OnSwitchDeactivated;
        public static event Action<List<FrameInput>> OnEchoRecorded;

        public static void TriggerSwitchActivated(int switchId) => OnSwitchActivated?.Invoke(switchId);
        public static void TriggerSwitchDeactivated(int switchId) => OnSwitchDeactivated?.Invoke(switchId);
        public static void TriggerEchoRecorded(List<FrameInput> recordedData) => OnEchoRecorded?.Invoke(recordedData);
    }
}
