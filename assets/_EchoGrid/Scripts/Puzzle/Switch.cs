using UnityEngine;
using EchoGrid.Interaction;
using EchoGrid.Events;
using EchoGrid.Player;

namespace EchoGrid.Puzzle
{
    public class Switch : MonoBehaviour, IInteractable
    {
        [Header("Switch Settings")]
        [Tooltip("A unique ID used to link to completely decoupled doors.")]
        [SerializeField] private int switchId;
        
        [Tooltip("If true, the player can turn the switch on and off repeatedly.")]
        [SerializeField] private bool toggleable = false;

        public bool IsActive { get; private set; }
        
        // It is interactable if it hasn't been activated yet, or if it can be toggled
        public bool IsInteractable => !IsActive || toggleable;

        public void Interact(PlayerController player)
        {
            if (IsActive && toggleable)
            {
                // Deactivate the switch
                IsActive = false;
                EventBus.TriggerSwitchDeactivated(switchId);
                Debug.Log($"Switch {switchId} Deactivated!");
            }
            else if (!IsActive)
            {
                // Activate the switch
                IsActive = true;
                EventBus.TriggerSwitchActivated(switchId);
                Debug.Log($"Switch {switchId} Activated!");
            }
            
            // Note: Visual updates like changing material from Red to Green can be added here later
        }
    }
}
