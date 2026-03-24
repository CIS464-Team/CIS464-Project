using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public string targetSceneName;
    private void OnTriggerEnter2D(Collider2D other) {
        print("Transition Trigger entered");
        if(other.tag == "Player") 
        {
            SceneController.Instance
                .NewTransition()
                .Load(SceneDatabase.Slots.SessionContent, targetSceneName, setActive:true)
                .Perform();
        }
    }

    public void CheatTP(string cheatSceneName) {
        print("GOD MODE: Teleporting to " + cheatSceneName);
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, cheatSceneName, setActive:true)
            .Perform();
    }
}