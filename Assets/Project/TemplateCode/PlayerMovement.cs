using UnityEngine;
using UnityEngine.InputSystem;

/*
 * To Apply this script: Set the variables serialized in the inspector. Jumping without setting the groundCheck or the groundLayer
 *  migth cause errors, so please refrain from doing it. 
 *  
 *  This script works best when untouched. To interact with it, you can get the movement variable and use DisableOrEnableInput.
 *  
 *  This script glues the necessary components with it, so you get the entire working component from the start.
 *  
 *  PS: If using this script in a version other than Unity 6, manually include the InputMapActions in PlayerInput.
 *  
 */



namespace Pedroca2005BR.Platformer_2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]

        [Tooltip("The speed modifier of the player.")]
        [SerializeField] float moveSpeed = 5;
        [Tooltip("If true, the script will automatically flip the sprite when changing directions.\nIf false, neither the sprite nor the gameObject will flip on its own.")]
        [SerializeField] bool canDirectionFlip;

        public Vector2 movement {  get; private set; }
        public float speedModifier { get; private set; } = 1f;


        [Header("Jump")]

        [Tooltip("A child gameObject sligthly beneath the feet of the player.")]
        [SerializeField] Transform groundCheck;
        [Tooltip("The ground layer (needed to recharge jumps).")]
        [SerializeField] LayerMask groundLayer;
        [Tooltip("The force of the jump.")]
        [SerializeField] int jumpForce = 400;
        [Tooltip("1 for double-jump;\n2 for triple-jump;\netc...")]
        [SerializeField] int numberOfExtraJumps = 0;
        [Space]
        [Tooltip("Forgives a little time of delay on pressing the jump button.\nIf not needed, just make it 0.")]
        [SerializeField][Range(0.05f, 2f)] float coyoteTimeAmount;
        //[Tooltip("Forgives the press of the jump button in advance.\nIf not needed, just make it 0.")]
        //[SerializeField][Range(0.05f, 2f)] float jumpBufferTime;

        int currentJumpCount = 0;
        protected float? lastGroundedTime;
        public float jumpMultiplier { get; private set; } = 1f;

        [Header("Material")]
        [SerializeField][Range(0f, 1f)] float friction;
        [SerializeField][Range(0f, 1f)] float bounciness;


        protected Rigidbody2D rb;
        PlayerInput playerInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput>();

            rb.freezeRotation = true;

            // Material settings
            PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D("Player_2D_Material");
            physicsMaterial2D.friction = friction;
            physicsMaterial2D.bounciness = bounciness;

            rb.sharedMaterial = physicsMaterial2D;
        }


        private void FixedUpdate()
        {
            if (CheckForGround())
            {
                lastGroundedTime = Time.time;
            }

            Move();
        }



        public void DisableOrEnableInput()
        {
            playerInput.enabled = !playerInput.enabled;
        }
        public void DisableOrEnableInput(bool b)
        {
            playerInput.enabled = b;
        }


        protected void Move()
        {
            rb.linearVelocity = new Vector2(movement.x * moveSpeed * speedModifier, rb.linearVelocity.y);

            bool isRunning = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;

            if (isRunning && canDirectionFlip)
            {
                Flip();
            }
        }

        void Flip()
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), transform.localScale.y);
        }

        protected bool CheckForGround()
        {
            return Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer);
        }

        void Jump()
        {
             rb.AddForceY(jumpForce * jumpMultiplier);
             lastGroundedTime = null;
        }

        public void ChangeSpeedModifier(float newSpeedModifier)
        {
            speedModifier = newSpeedModifier;

            EventManager.TriggerEvent("ChangeSpeedModifier", new float[] { speedModifier, jumpMultiplier });
        }

        public void ChangeJumpModifier(float newjumpModifier)
        {
            jumpMultiplier = newjumpModifier;

            EventManager.TriggerEvent("ChangeJumpModifier", new float[] { speedModifier, jumpMultiplier });
        }




        void OnMove(InputValue input)
        {
            movement = input.Get<Vector2>();
        }

        void OnJump(InputValue input)
        {
            if (groundCheck == null)
            {
                Debug.LogWarning("Player with no Ground Check!", gameObject);
                return;
            }

            // Coyote Time implementation
            if (Time.time - lastGroundedTime <= coyoteTimeAmount)
            {
                Jump();
            }
            else if (currentJumpCount < numberOfExtraJumps)
            {
                currentJumpCount++;
                rb.linearVelocityY = 0;
                Jump();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.5f, 0.1f));
        }
    }
}
