using UnityEngine;

namespace TAG.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CreatureController : MonoBehaviour
    {
        [SerializeField] private CreatureDefinition definition;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem dashBurst;
        [SerializeField] private float gravity = -28f;
        [SerializeField] private float turnSpeed = 18f;

        private CharacterController controller;
        private Vector3 velocity;
        private int jumpsRemaining = 2;

        public CreatureDefinition Definition => definition;
        public Vector3 Velocity => velocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        public void Move(Vector2 input, bool sprint, bool jumpPressed, bool dashPressed)
        {
            var cameraForward = Camera.main != null ? Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f)).normalized : Vector3.forward;
            var cameraRight = Camera.main != null ? Camera.main.transform.right : Vector3.right;
            var desired = cameraForward * input.y + cameraRight * input.x;
            desired = Vector3.ClampMagnitude(desired, 1f);

            var speed = definition != null ? definition.moveSpeed : 8f;
            if (sprint) speed *= definition != null ? definition.sprintMultiplier : 1.35f;

            velocity.x = desired.x * speed;
            velocity.z = desired.z * speed;

            if (controller.isGrounded)
            {
                jumpsRemaining = 2;
                if (velocity.y < 0f) velocity.y = -2f;
            }

            if (jumpPressed && jumpsRemaining > 0)
            {
                velocity.y = definition != null ? definition.jumpForce : 8f;
                jumpsRemaining--;
                animator?.SetTrigger("Jump");
            }

            if (dashPressed && desired.sqrMagnitude > 0.01f)
            {
                var force = definition != null ? definition.dashForce : 16f;
                velocity += desired.normalized * force;
                dashBurst?.Play();
                animator?.SetTrigger("Dash");
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (desired.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired), turnSpeed * Time.deltaTime);
            }

            animator?.SetFloat("Speed", new Vector2(velocity.x, velocity.z).magnitude);
            animator?.SetBool("Grounded", controller.isGrounded);
        }
    }
}
