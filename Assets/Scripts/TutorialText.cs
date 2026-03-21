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
    [SerializeField] private string message = "press E to read";
    [SerializeField] private float fadeDuration = 1.0f;

    // Player Control section in inspector
    [Header("Player Control")]
    [SerializeField] private PlayerMovement playerMovementScript; 
    private Animator playerAnimator;

    private bool isWaitingForInput = false;

    // Called whenever script is loaded
    void Awake()
    {
        // Make tutorial text transparent initially
        if (canvasGroup != null) canvasGroup.alpha = 0;
        // Assign the user's string message to the TextMeshPro component
        if (textElement != null) textElement.text = message;
    }

    // Triggered by a signal at the end of TutorialCutscene
    public void StartTutorialPrompt()
    {
        // Look for PlayerMovement script inside Player GameObject
        if (playerMovementScript != null) 
        {
            // Temporarily disables the movement script 
            // Triggers a 'if(!enabled)' check in PlayerMovement.cs
            playerMovementScript.enabled = false;
            
            // Create reference to Player GameObject that holds the script
            GameObject playerObj = playerMovementScript.gameObject;

            // Set Player's Rigidbody2D velocity to zero
            if (playerObj.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = Vector2.zero;
            }

            // Temporarily pause the player animations by setting Animator speed to 0
            if (playerObj.TryGetComponent<Animator>(out playerAnimator))
            {
                playerAnimator.speed = 0;
            }
        }
        
        // Stop any active fade routines to prevent conflicts
        StopAllCoroutines(); 

        // Set waitingForInput to true after fade-in completes
        StartCoroutine(FadeCanvas(1f, () => {isWaitingForInput = true;}));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we are waiting for input AND if E was pressed
        if (isWaitingForInput && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Set flag to false
            isWaitingForInput = false;
            
            // Start text fadeout and re-enable player controls once fade is complete
            StartCoroutine(FadeCanvas(0f, () => {
                
                // Reenable the movement script
                if (playerMovementScript != null) playerMovementScript.enabled = true;
                
                // Resume animations
                if (playerAnimator != null) playerAnimator.speed = 1;
                
                // Disable Canvas once finished with tutorial prompt
                canvasGroup.gameObject.SetActive(false); 
            }));
        }
    }

    // Fades canvas in or out based on targetAlpha
    private IEnumerator FadeCanvas(float targetAlpha, System.Action onComplete = null)
    {
        // Store the alpha value we are starting from
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        // Loop until the timer reaches the set fade duration
        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            // Interpolate the alpha value based on the percentage of time passed
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);

            // Wait until the next frame before continuing the loop
            yield return null;
        }

        // Ensure targetAlpha is reached
        canvasGroup.alpha = targetAlpha;

        // Execute code inside callback after fade completes
        onComplete?.Invoke();
    }
}