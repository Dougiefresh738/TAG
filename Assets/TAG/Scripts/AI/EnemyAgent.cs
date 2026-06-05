using UnityEngine;
using UnityEngine.AI;

namespace TAG.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class EnemyAgent : MonoBehaviour
    {
        [SerializeField] private AIDifficultyProfile profile;
        [SerializeField] private Animator animator;
        private NavMeshAgent agent;
        private Vector3 lastKnownTarget;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void Pursue(Transform target, Vector3 squadOffset, float aggression)
        {
            if (target == null || profile == null) return;
            var predicted = target.position;
            if (profile.enableGodModePrediction)
            {
                predicted += target.forward * Mathf.Lerp(2f, 8f, aggression);
            }

            lastKnownTarget = predicted;
            agent.speed = profile.chaseSpeed * aggression;
            agent.SetDestination(predicted + squadOffset);
            animator?.SetFloat("Speed", agent.velocity.magnitude);
        }

        public void Search()
        {
            agent.speed = profile != null ? profile.patrolSpeed : 4f;
            agent.SetDestination(lastKnownTarget);
        }
    }
}
