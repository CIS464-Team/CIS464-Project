using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EnterTunnel : MonoBehaviour
{
    public KeyArea keyArea;
    private PlayerMovement playerMovement;
    public float autoWalkDuration = 1f;
    public bool tunnelOpen = false;

    [Header("Tunnel Transition")]
    public float fadeDuration = 0.5f;
    public float teleportOffset = 50f; // positive = south entry (up), negative = north entry (down)
    public SpriteRenderer darknessOverlay; // assign a black UI panel's SpriteRenderer in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Player entered tunnel");
            

            playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement == null) return;
            if(Keyboard.current.wKey.isPressed) {
                StartCoroutine(TunnelTransition(Vector2.up, 56f));
            }
            if(Keyboard.current.sKey.isPressed) {
                StartCoroutine(TunnelTransition(Vector2.down, 4f));
            }
        }
    }

    private IEnumerator TunnelTransition(Vector2 direction, float y)
    {
        BoxCollider2D playerCollider = playerMovement.GetComponent<BoxCollider2D>();
        if (playerCollider != null) playerCollider.enabled = false;
        
        PlayerInput playerInput = playerMovement.GetComponent<PlayerInput>();
        playerInput.enabled = false;
        playerMovement.SetInputLocked(true);

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Teleport player
        Vector3 pos = playerMovement.transform.position;
        playerMovement.transform.position = new Vector3(-.5f, y, pos.z);

        // Brief pause in darkness
        yield return new WaitForSeconds(1f);

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));
        
        if (!tunnelOpen)
            {
                print("Opening tunnel...");
                keyArea.OpenTunnel();
                tunnelOpen = true;

                yield return new WaitForSeconds(1f);
                // Auto-walk after reappearing
                yield return StartCoroutine(AutoWalk(direction));

                playerInput.enabled = true;
                playerMovement.SetInputLocked(false);
                yield return new WaitForSeconds(.5f);
                if (playerCollider != null) playerCollider.enabled = true;
            } else {
                yield return new WaitForSeconds(.4f);
                yield return StartCoroutine(AutoWalk(direction));
                playerInput.enabled = true;
                playerMovement.SetInputLocked(false);
                if (playerCollider != null) playerCollider.enabled = true;
            }

    }

    private IEnumerator Fade(float from, float to)
    {   
        float elapsed = 0f;
        SetOverlayAlpha(from);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetOverlayAlpha(Mathf.Lerp(from, to, elapsed / fadeDuration));
            yield return null;
        }

        SetOverlayAlpha(to);
    }

    private void SetOverlayAlpha(float alpha)
    {
        Color c = darknessOverlay.color;
        c.a = alpha;
        darknessOverlay.color = c;
    }
    private IEnumerator AutoWalk(Vector2 direction)
    {
        float elapsed = 0f;

        while (elapsed < autoWalkDuration)
        {
            playerMovement.ApplyMovement(direction * 2f);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}