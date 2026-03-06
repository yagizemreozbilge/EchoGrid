using System.Collections.Generic;
using EchoGridLibrary.Events;

namespace EchoGridLibrary.Puzzle
{
    public class DoorLogic
    {
        public List<int> RequiredSwitchIds { get; set; } = new List<int>();
        private HashSet<int> _activeSwitches = new HashSet<int>();
        
        public bool IsOpen { get; private set; }

        public void HandleSwitchActivated(int switchId)
        {
            if (RequiredSwitchIds.Contains(switchId))
            {
                _activeSwitches.Add(switchId);
                CheckDoorState();
            }
        }

        public void HandleSwitchDeactivated(int switchId)
        {
            if (RequiredSwitchIds.Contains(switchId))
            {
                _activeSwitches.Remove(switchId);
                CheckDoorState();
            }
        }

        public void CheckDoorState()
        {
            if (_activeSwitches.Count >= RequiredSwitchIds.Count && !IsOpen)
            {
                IsOpen = true;
            }
            else if (_activeSwitches.Count < RequiredSwitchIds.Count && IsOpen)
            {
                IsOpen = false;
            }
        }
    }
}
