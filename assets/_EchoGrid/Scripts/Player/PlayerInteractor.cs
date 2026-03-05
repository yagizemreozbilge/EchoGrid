using UnityEngine;
using EchoGrid.Interaction;

namespace EchoGrid.Player
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInteractor : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactionRadius = 2f;
        [SerializeField] private LayerMask interactableLayer;

        private InputHandler _inputHandler;
        private PlayerController _playerController;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (_inputHandler.InteractPressed)
            {
                TryInteract();
            }
        }

        private void TryInteract()
        {
            // Physics.OverlapSphere finds all colliders within a certain radius of our character
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);
            
            foreach (var hitCollider in hitColliders)
            {
                IInteractable interactableObject = hitCollider.GetComponent<IInteractable>();
                
                if (interactableObject != null && interactableObject.IsInteractable)
                {
                    interactableObject.Interact(_playerController);
                    // Only interact with the first valid object we find
                    break;
                }
            }
        }

        // Draw a gizmo in the Unity Editor to visualize the interaction radius
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
