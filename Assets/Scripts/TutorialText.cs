using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{
    // UI References section in inspector
    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI textElement;

    // Settings section in inspector
    [Header("Settings")]
    [SerializeField] private string readMessage = "Press E To Read";
    [SerializeField] private string movementMessage = "WASD/Arrows";
    [SerializeField] private float fadeDuration = 1.5f;

    // Player Control section in inspector
    [Header("Player Control")]
    [SerializeField] private PlayerMovement playerMovementScript; 
    [SerializeField] private RuntimeAnimatorController playerController; 
    private Animator playerAnimator;

    // Managers section in inspector
    [Header("Managers")]
    [SerializeField] private TimelineControl timelineControl;

    private bool isWaitingForE = false;
    private bool isWaitingForAnyKey = false;

    // Called whenever script is loaded
    void Awake()
    {
        // Make tutorial text transparent initially
        if (canvasGroup != null) canvasGroup.alpha = 0;
        // Assign the user's string message to the TextMeshPro component
        if (textElement != null) textElement.text = readMessage;
    }

    // Triggered by the first signal at the start of the letter interaction
    public void StartTutorialPrompt()
    {
        // Pause the timeline playhead immediately
        if (timelineControl != null) timelineControl.PauseTimeline();
        
        // Look for PlayerMovement script inside Player GameObject
        if (playerMovementScript != null) {
            // Temporarily disables the movement script 
            playerMovementScript.enabled = false;

            // Set Player's Rigidbody2D velocity to zero
            if (playerMovementScript.TryGetComponent<Rigidbody2D>(out var rb)) 
            {
                rb.linearVelocity = Vector2.zero;
            }

            // Temporarily pause the player animations
            if (playerMovementScript.TryGetComponent<Animator>(out playerAnimator))
            {
                playerAnimator.speed = 0;
            }
        }

        // Stop any active fade routines to prevent conflicts
        StopAllCoroutines(); 

        // Set isWaitingForE to true after fade-in completes
        StartCoroutine(FadeCanvas(1f, () => isWaitingForE = true));
    }

    // Triggered by the second signal once the paper has fully risen
    public void StartAnyKeyPrompt()
    {
        // Pause the timeline again to hold the paper on screen
        if (timelineControl != null) timelineControl.PauseTimeline();

        // Change the text to the movement instructions
        if (textElement != null) textElement.text = movementMessage;

        // Fade the text back in and wait for any key press
        StartCoroutine(FadeCanvas(1f, () => isWaitingForAnyKey = true));
    }

    // Update is called once per frame
    void Update()
    {
        // First, player presses E to start the paper rising animation
        if (isWaitingForE && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isWaitingForE = false;
            
            // Fade text out and resume timeline playhead
            StartCoroutine(FadeCanvas(0f, () => {
                if (timelineControl != null) timelineControl.ResumeTimeline();
            }));
        }

        // Second, player presses any key to finish the tutorial
        if (isWaitingForAnyKey && Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            isWaitingForAnyKey = false;
            
            // Resume timeline for the final part of the animation
            if (timelineControl != null) timelineControl.ResumeTimeline();

            // Reenable player movement and reassign the Animator Controller
            if (playerMovementScript != null) {
                playerMovementScript.enabled = true;
                
                if (playerMovementScript.TryGetComponent<Animator>(out playerAnimator))
                {
                    // Plugs the walking/idle logic back into the player animator
                    playerAnimator.runtimeAnimatorController = playerController;
                    playerAnimator.speed = 1;
                }
            }

            // Start monitoring for movement to fade the text out permanently
            StartCoroutine(FadeAfterMovement());
        }
    }

    // Wait for movement before final fade out
    private IEnumerator FadeAfterMovement()
    {
        // Wait until the player presses any of the movement keys
        yield return new WaitUntil(() => 
            Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || 
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed);
        
        // Final fade out of the tutorial text
        yield return StartCoroutine(FadeCanvas(0f));
    }

    // Fades canvas in or out based on targetAlpha
    private IEnumerator FadeCanvas(float targetAlpha, System.Action onComplete = null)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            // Interpolate the alpha value based on time passed
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        onComplete?.Invoke();
    }
}