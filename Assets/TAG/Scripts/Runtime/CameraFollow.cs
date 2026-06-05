using UnityEngine;

namespace TAG.Runtime
{
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 9f, -10f);
        [SerializeField] private float smoothTime = 0.12f;
        private Vector3 velocity;

        public void SetTarget(Transform newTarget) => target = newTarget;

        private void LateUpdate()
        {
            if (target == null) return;
            var desired = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position + Vector3.up - transform.position), 12f * Time.deltaTime);
        }
    }
}
