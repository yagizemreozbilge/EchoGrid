using UnityEngine;
using EchoGrid.Events;

namespace EchoGrid.Puzzle
{
    // Lasers act as logic gates: If player touches -> death/reset. 
    // If EchoClone touches -> blocks laser so player can pass!
    [RequireComponent(typeof(LineRenderer))]
    public class LaserEmitter : MonoBehaviour
    {
        [Header("Laser Settings")]
        [SerializeField] private float maxDistance = 50f;
        
        [Tooltip("What layers can block the laser? Includes Environment, Player, and EchoClones.")]
        [SerializeField] private LayerMask obstacleLayer;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            
            // A laser is essentially a straight line between 2 points
            _lineRenderer.positionCount = 2;
        }

        private void Update()
        {
            FireLaser();
        }

        private void FireLaser()
        {
            // Start point of the laser is always the emitter itself
            _lineRenderer.SetPosition(0, transform.position);

            Vector3 direction = transform.forward;
            RaycastHit hit;

            // Shoot a physical raycast into the world
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance, obstacleLayer))
            {
                // The laser line stops exactly where it hits something
                _lineRenderer.SetPosition(1, hit.point);

                // Check WHAT the laser hit
                if (hit.collider.CompareTag("Player"))
                {
                    // For now we just log, but normally this triggers a Level/Room restart Event!
                    Debug.LogWarning("Player was hit by a laser! (Puzzle Failed / Reset)");
                }
                else if (hit.collider.CompareTag("EchoClone"))
                {
                    // The EchoClone absorbs the damage/beam, creating a safe shadow for the player!
                    // This is a core puzzle mechanic.
                }
            }
            else
            {
                // If it hits nothing, shoot to the absolute max distance into the void
                _lineRenderer.SetPosition(1, transform.position + direction * maxDistance);
            }
        }
    }
}
