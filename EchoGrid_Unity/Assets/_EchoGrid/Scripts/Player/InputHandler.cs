using UnityEngine;

namespace EchoGrid.Player
{
    public class InputHandler : MonoBehaviour
    {
        public Vector3 MovementInput { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool RecordPressed { get; private set; }

        private void Update()
        {
            // Gather input for movement (mapping to X and Z for 3D Top-Down)
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            // Normalize so diagonal movement isn't faster
            MovementInput = new Vector3(horizontal, 0f, vertical).normalized;

            // Gathering other inputs early to be ready for future stages
            InteractPressed = Input.GetKeyDown(KeyCode.E);
            RecordPressed = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
