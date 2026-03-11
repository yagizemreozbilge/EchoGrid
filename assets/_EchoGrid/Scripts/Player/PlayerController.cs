using UnityEngine;

namespace EchoGrid.Player
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float rotationSpeed = 12f;

        private InputHandler _inputHandler;
        private Rigidbody _rb;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _rb = GetComponent<Rigidbody>();
            
            // Prevent our character from tipping over like a physics doll
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void FixedUpdate()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            Vector3 movement = _inputHandler.MovementInput * moveSpeed;
            
            // We set velocity directly for snappy, tight controls 
            // but keep the Y velocity intact for gravity.
            _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);
        }

        private void RotatePlayer()
        {
            // Only rotate if we are actually moving
            if (_inputHandler.MovementInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_inputHandler.MovementInput, Vector3.up);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
