using System;
using System.Collections.Generic;

namespace EchoGridLibrary.Events
{
    public struct FrameInput
    {
        public Core.Vector3 position;
        public Core.Quaternion rotation;
        public float timestamp;
    }

    public static class EventBus
    {
        public static event Action<int>? OnSwitchActivated;
        public static event Action<int>? OnSwitchDeactivated;
        public static event Action<List<FrameInput>>? OnEchoRecorded;

        public static void TriggerSwitchActivated(int switchId) => OnSwitchActivated?.Invoke(switchId);
        public static void TriggerSwitchDeactivated(int switchId) => OnSwitchDeactivated?.Invoke(switchId);
        public static void TriggerEchoRecorded(List<FrameInput> recordedData) => OnEchoRecorded?.Invoke(recordedData);
    }
}
