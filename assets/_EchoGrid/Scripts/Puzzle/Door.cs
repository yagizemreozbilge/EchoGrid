using System.Collections.Generic;
using UnityEngine;
using EchoGrid.Events;

namespace EchoGrid.Puzzle
{
    public class Door : MonoBehaviour
    {
        [Header("Door Settings")]
        [Tooltip("The IDs of the switches that must all be active to open this door.")]
        [SerializeField] private List<int> requiredSwitchIds = new List<int>();

        // We use a HashSet because it avoids duplicates naturally, tracking only unique Active triggers
        private HashSet<int> _activeSwitches = new HashSet<int>();
        
        public bool IsOpen { get; private set; }

        private void OnEnable()
        {
            // Subscribe to our global EventBus when this object exists
            EventBus.OnSwitchActivated += HandleSwitchActivated;
            EventBus.OnSwitchDeactivated += HandleSwitchDeactivated;
        }

        private void OnDisable()
        {
            // Unsubscribe when destroyed or disabled to prevent memory leaks
            EventBus.OnSwitchActivated -= HandleSwitchActivated;
            EventBus.OnSwitchDeactivated -= HandleSwitchDeactivated;
        }

        private void HandleSwitchActivated(int switchId)
        {
            if (requiredSwitchIds.Contains(switchId))
            {
                _activeSwitches.Add(switchId);
                CheckDoorState();
            }
        }

        private void HandleSwitchDeactivated(int switchId)
        {
            if (requiredSwitchIds.Contains(switchId))
            {
                _activeSwitches.Remove(switchId);
                CheckDoorState();
            }
        }

        // We made this public so our Unit Tests can test logic independently of physics/events
        public void CheckDoorState()
        {
            // If all required switches are active, open the door
            if (_activeSwitches.Count >= requiredSwitchIds.Count && !IsOpen)
            {
                OpenDoor();
            }
            // If the switches are no longer fully met, close the door 
            else if (_activeSwitches.Count < requiredSwitchIds.Count && IsOpen)
            {
                CloseDoor();
            }
        }

        private void OpenDoor()
        {
            IsOpen = true;
            Debug.Log($"Door {gameObject.name} Unlocked and Opened!");
            
            // Temporary placeholder logic for "opening"
            // (Usually we play an animation or use Vector3.MoveTowards)
            gameObject.SetActive(false); 
        }

        private void CloseDoor()
        {
            IsOpen = false;
            Debug.Log($"Door {gameObject.name} Locked and Closed!");
            
            gameObject.SetActive(true);
        }
    }
}
