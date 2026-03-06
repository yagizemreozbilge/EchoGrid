using System.Collections.Generic;
using EchoGridLibrary.Core;

namespace EchoGridLibrary.Enemy
{
    public class EnemyBrain
    {
        public enum State { Patrolling, Chasing }
        public State CurrentState { get; set; } = State.Patrolling;

        public float ChaseRange { get; set; } = 10f;

        public void DecideState(float distanceToPlayer)
        {
            if (distanceToPlayer < ChaseRange)
            {
                CurrentState = State.Chasing;
            }
            else
            {
                CurrentState = State.Patrolling;
            }
        }

        public int GetNextWaypoint(int currentIndex, int totalWaypoints, bool hasReachedTarget)
        {
            if (totalWaypoints == 0) return 0;
            if (hasReachedTarget)
            {
                return (currentIndex + 1) % totalWaypoints;
            }
            return currentIndex;
        }
    }
}
