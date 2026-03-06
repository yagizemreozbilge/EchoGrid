using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace EchoGrid.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        public enum EnemyState { Idle, Patrolling, Chasing }

        [Header("AI Settings")]
        [SerializeField] private EnemyState currentState = EnemyState.Patrolling;
        [SerializeField] private float chaseRange = 10f;
        [SerializeField] private float patrollingSpeed = 3.5f;
        [SerializeField] private float chasingSpeed = 5.5f;

        [Header("Patrol Waypoints")]
        [SerializeField] private List<Transform> patrolWaypoints = new List<Transform>();
        private int _currentWaypointIndex = 0;

        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            // In a real project, we might use an Event or a global manager to find the player, 
            // but for simple logic, we find the tag.
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) _playerTransform = player.transform;
        }

        private void Update()
        {
            if (_playerTransform == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

            // State Transition Logic
            if (distanceToPlayer < chaseRange)
            {
                currentState = EnemyState.Chasing;
            }
            else
            {
                currentState = EnemyState.Patrolling;
            }

            // Execute State Action
            switch (currentState)
            {
                case EnemyState.Patrolling:
                    Patrol();
                    break;
                case EnemyState.Chasing:
                    ChasePlayer();
                    break;
            }
        }

        private void Patrol()
        {
            if (patrolWaypoints.Count == 0) return;

            _agent.speed = patrollingSpeed;
            
            // Move toward current waypoint
            _agent.SetDestination(patrolWaypoints[_currentWaypointIndex].position);

            // If we reached the waypoint, switch to the next one
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % patrolWaypoints.Count;
            }
        }

        private void ChasePlayer()
        {
            _agent.speed = chasingSpeed;
            _agent.SetDestination(_playerTransform.position);
            
            // If the enemy touches the player, we would normally trigger a Game Over event here
        }

        // Visualize the chase range in Editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
