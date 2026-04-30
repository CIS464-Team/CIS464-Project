using UnityEngine;
using System.Collections;

public class SecondPuzzle : MonoBehaviour
{
    
    public GameObject block1;
    public GameObject block2;
    public GameObject laserGoal;
    void Start()
    {
        block1.transform.localPosition = new Vector3(19.5f, 4.4f, 0);
        block2.transform.localPosition = new Vector3(19.5f, 3.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (laserGoal.GetComponent<LaserGoal>().isHit)
        {
        StartCoroutine(MoveBlock(block1, new Vector3(19.5f, 6f, 0)));
        StartCoroutine(MoveBlock(block2, new Vector3(19.5f, 2f, 0)));
        }  
    }
    
    private IEnumerator MoveBlock(GameObject block, Vector3 targetPos)
    {
        Vector3 startPos = block.transform.localPosition;
        float duration = 1f; // seconds
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            block.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            yield return null;
        }

        block.transform.localPosition = targetPos; // snap to exact final position
    }
}
