using UnityEngine;
using EchoGrid.Events;

namespace EchoGrid.Puzzle
{
    [RequireComponent(typeof(Collider))]
    public class PressurePlate : MonoBehaviour
    {
        [Header("Pressure Plate Settings")]
        [Tooltip("The unique ID representing this switch in the EventBus.")]
        [SerializeField] private int switchId;

        // Track how many objects are pressing the plate (Player + Clones)
        // If a Player and a Clone are both on the plate, it stays active when the Player leaves.
        private int _objectsOnPlate = 0;
        public bool IsActive => _objectsOnPlate > 0;

        private void Awake()
        {
            // Ensure the collider is a trigger so things can step *into* it
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // We expect the Player and the EchoClones to have these tags in Unity
            if (other.CompareTag("Player") || other.CompareTag("EchoClone"))
            {
                _objectsOnPlate++;
                
                // If this is the FIRST object to step on the plate, activate it
                if (_objectsOnPlate == 1)
                {
                    EventBus.TriggerSwitchActivated(switchId);
                    Debug.Log($"Pressure Plate {switchId} Pressed by {other.name}!");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("EchoClone"))
            {
                _objectsOnPlate--;
                
                // Prevent negative counts just in case of edge-case physics bugs
                if (_objectsOnPlate < 0) _objectsOnPlate = 0;

                // If the LAST object has left the plate, deactivate it
                if (_objectsOnPlate == 0)
                {
                    EventBus.TriggerSwitchDeactivated(switchId);
                    Debug.Log($"Pressure Plate {switchId} Released!");
                }
            }
        }
    }
}
