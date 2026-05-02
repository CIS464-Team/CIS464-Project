using UnityEngine;
using System.Collections;

public class FirstPuzzle : MonoBehaviour
{
    
    public GameObject block1;
    public GameObject block2;
    public GameObject laserGoal;
    private Transform[] movablePieces;
    private Vector3[] startPositions;
    public bool canReset = false;
    void Start()
    {
        block1.transform.localPosition = new Vector3(-2.5f, 5.683f, 0);
        block2.transform.localPosition = new Vector3(-1.5f, 5.683f, 0);
        // Skip index 0 since that's FirstPuzzle itself
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        movablePieces = new Transform[allChildren.Length - 1];
        for (int i = 1; i < allChildren.Length; i++)
            movablePieces[i - 1] = allChildren[i];

        // Snapshot after Start() has placed everything
        startPositions = new Vector3[movablePieces.Length];
        for (int i = 0; i < movablePieces.Length; i++)
            startPositions[i] = movablePieces[i].localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if any of the reflectors have been moved from their starting positions, and if so, allow the player to reset the puzzle
        if (!canReset)
        {
            for (int i = 0; i < movablePieces.Length; i++)
            {
                if (movablePieces[i].localPosition != startPositions[i])
                {
                    canReset = true;
                    break;
                }
            }
        }
        if (laserGoal.GetComponent<LaserGoal>().isHit)
        {
        StartCoroutine(MoveBlock(block1, new Vector3(-3.7f, 5.683f, 0)));
        StartCoroutine(MoveBlock(block2, new Vector3(-.3f, 5.683f, 0)));
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

    public void ResetPuzzle()
    {
        print("Resetting Area 4's First Puzzle");
        for (int i = 0; i < movablePieces.Length; i++)
            movablePieces[i].localPosition = startPositions[i];
    }   
}
