using UnityEditor.ShaderGraph;
using UnityEngine;

public class MossCollector : MonoBehaviour
{
    public GameObject mossText;
    private float timer = 0;
    private bool canFade = true;
    private bool fadeOut = false;

    void Update()
    {
        if (canFade && fadeOut)
        {
            // show text
            mossText.SetActive(true);

            // check if timer is still running
            if (timer < 5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // end animation
                fadeOut = false;
                canFade = false;

                mossText.SetActive(false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if we are in contact with a goal item
        if (collision.CompareTag("Player") )
        {
            Debug.Log("Moss picked up");

            // play the key obtain sound
            soundManager.Instance.PlaySFX("Moss");

            fadeOut = true;
        }
    }
}
