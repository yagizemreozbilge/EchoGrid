using UnityEngine;

namespace EchoGrid.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Transform target;
        // The default offset creates a typical 3D top-down / isometric angle
        [SerializeField] private Vector3 offset = new Vector3(0, 15, -10);
        [SerializeField] private float smoothSpeed = 5f;

        private void LateUpdate()
        {
            if (target == null) return;

            // This calculates where the camera should try to be standing
            Vector3 desiredPosition = target.position + offset;
            
            // Smoothly move the camera step by step toward the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            
            // Ensure camera is always aiming straight at the player
            transform.LookAt(target.position);
        }
        
        // This is extremely helpful for debugging and setting up the camera later inside the Unity Editor
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}
