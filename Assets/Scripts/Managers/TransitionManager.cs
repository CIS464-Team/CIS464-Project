using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public string targetSceneName;

    private PlayerMovement playerMovement;
    public float autoWalkDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        print("Transition Trigger entered");
        if(other.tag == "Player") 
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement == null) return;
            Vector2 entryDirection = playerMovement.MoveInput.normalized;
            SceneController.Instance
                .NewTransition()
                .Load(SceneDatabase.Slots.SessionContent, targetSceneName, setActive:true)
                .WithOverlay()
                .Perform();
            StartCoroutine(AutoWalk(entryDirection));
        }
    }

    private IEnumerator AutoWalk(Vector2 direction)
    {
        float elapsed = 0f;

        // Lock player input during auto-walk
        playerMovement.SetInputLocked(true);

        while (elapsed < autoWalkDuration)
        {
            playerMovement.ApplyMovement(direction);
            elapsed += Time.deltaTime;
            yield return null;
        }

        playerMovement.SetInputLocked(false);
    }

    public void CheatTP(string cheatSceneName) {
        print("GOD MODE: Teleporting to " + cheatSceneName);
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, cheatSceneName, setActive:true)
            .Perform();
    }
}
