using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace YetAnotherUnityShooter
{
    public class PlayerMotor : MonoBehaviour
    {
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        public float baseJump = 1f;                     // Player's jump height [ BASE ]::      100%
        public float baseSpeed = 5f;                    // Player's speed [ BASE ]::            100%
        /*
        public float WalkSpeed => BaseSpeed * 0.3f;     // Player's speed [ WALK ]::            30%
        public float CrouchSpeed => BaseSpeed * 0.6f;   // Player's speed [ CROUCH ]::          60%
        public float ProneSpeed => BaseSpeed * 0.15f;   // Player's speed [ PRONE ]::           15%
        public float AimDownSpeed => BaseSpeed * 0.5f;  // Player's speed [ AIMING ]::          50%
        public float CrouchAndWalkSpeed => BaseSpeed;   // Player's speed [ CROUCH + WALK ]::   15%
        */
        public const float gravity = -9.8f;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = controller.isGrounded;
        }
        public void ProcessMovement(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(baseSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);
#if DEBUG
            Debug.Log(playerVelocity.y);
#endif
        }
        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(baseJump * -3.0f * gravity);
            }
        }
    }
}