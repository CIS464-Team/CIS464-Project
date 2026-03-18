using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove1 : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;
    
    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger enttered");
        if(other.tag == "Player") {
            print("switching scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
