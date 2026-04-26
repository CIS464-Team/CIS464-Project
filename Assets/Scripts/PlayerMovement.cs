using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // declare variables
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private soundManager soundManager;
    private bool playingFootsteps;
    private bool onIce;
    private bool isSliding;
    public float footstepSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseManager.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero; // stop walking if we are paused\
            animator.SetBool("isWalking", false);
            return;
        }
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetBool("isWalking", rb.linearVelocity.magnitude > 0);

        //when player is moving play footstep audio and stop if their not
        if (rb.linearVelocity.magnitude > 0 && !playingFootsteps)
        {
            startFootsteps();
        } else if (rb.linearVelocity.magnitude == 0)
        {
            StopFootsteps();
        }   
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Only continue if PlayerMovement is enabled 
        if (!this.enabled) 
        {
            moveInput = Vector2.zero;
            return; 
        }

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        // check for ice physics
        if (onIce)
        {
            // check if we are currently sliding
            if (isSliding)
            {
                // check if we stopped (hit an object)
                if (rb.linearVelocity == Vector2.zero)
                {
                    isSliding = false; // exit sliding state
                }
            }
            else
            {
                moveInput = context.ReadValue<Vector2>();

                // only allow 4 directions on ice
                if (moveInput.x > 0)
                {
                    animator.SetFloat("InputX", moveInput.x);
                }
                else if (moveInput.y > 0)
                {
                    animator.SetFloat("InputY", moveInput.y);
                }
                
            }
        }
        else
        {
            // normal movement
            moveInput = context.ReadValue<Vector2>();

            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
        
    }

    void startFootsteps()
    //while bool is true keep calling Playfoorstep repeatedly
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
    }

    void StopFootsteps()
    {
    //once bool is false stop calling it
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }
    
    void PlayFootstep()
    {
        soundManager.Instance.PlaySFX("footsteps");
    }

    // Handle opening the debug console since unity hates multiple player inputs both sending msgs
    public void OnOpenDebugConsole(InputAction.CallbackContext context)
    {
        if (DebugController.Instance != null)
            DebugController.Instance.ToggleConsole();
    }

    public void OnReturn(InputAction.CallbackContext context)
    {
        if (DebugController.Instance != null)
            DebugController.Instance.HandleReturn();
    }

    // Handle opening the in-game menu since unity hates multiple player inputs both sending msgs
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (InGameMenuManager.Instance != null)
            InGameMenuManager.Instance.OpenSettings();
    }

    private bool inputLocked = false;
    public Vector2 MoveInput => inputLocked ? Vector2.zero : moveInput;

    public void SetInputLocked(bool locked)
    {
        inputLocked = locked;
    }

    public void ApplyMovement(Vector2 direction)
    {
        // Drive your rigidbody or transform directly
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        // OR: rb.velocity = direction * moveSpeed;
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if we are in contact with ice
        if (collision.CompareTag("ICE-E") )
        {
            // update onIce state
            onIce = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // check if we are in contact with ice
        if (collision.CompareTag("ICE-E") )
        {
            // update onIce state
            onIce = false;
        } 
    }
}
