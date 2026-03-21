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
}
