using UnityEngine;
using System.Collections;

public class SecondPuzzle : MonoBehaviour
{
    private bool hasMovedBlocks = false;
    public GameObject block1;
    public GameObject block2;
    public GameObject laserGoal;
    private Transform[] movablePieces;
    private Vector3[] startPositions;
    public bool canReset = false;
    void Start()
    {
        block1.transform.localPosition = new Vector3(19.5f, 4.4f, 0);
        block2.transform.localPosition = new Vector3(19.5f, 3.4f, 0);
        // Only keep track of children #3 and beyond (skip 0: self, 1: block1, 2: block2)
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        if (allChildren.Length > 3) {
            int trackedCount = allChildren.Length - 3;
            movablePieces = new Transform[trackedCount];
            for (int i = 3; i < allChildren.Length; i++)
                movablePieces[i - 3] = allChildren[i];
            // Snapshot after Start() has placed everything
            startPositions = new Vector3[trackedCount];
            for (int i = 0; i < trackedCount; i++)
                startPositions[i] = movablePieces[i].localPosition;
        } else {
            movablePieces = new Transform[0];
            startPositions = new Vector3[0];
        }

        if (Area4Manager.Instance.LaserGoalsHit[1])
        {
            hasMovedBlocks = true;
            block1.transform.localPosition = new Vector3(19.5f, 6f, 0);
            block2.transform.localPosition = new Vector3(19.5f, 2f, 0);
            laserGoal.GetComponent<LaserGoal>().isHit = true;
        }
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
        if (laserGoal.GetComponent<LaserGoal>().isHit && !hasMovedBlocks)
        {
            Area4Manager.Instance.LaserGoalsHit[1] = true;
            hasMovedBlocks = true;
            StartCoroutine(MoveBlock(block1, new Vector3(19.5f, 6f, 0)));
            StartCoroutine(MoveBlock(block2, new Vector3(19.5f, 2f, 0)));
        }
    }
    
    private IEnumerator MoveBlock(GameObject block, Vector3 targetPos)
    {
        Vector3 startPos = block.transform.localPosition;
        float duration = 1f; // seconds
        float elapsed = 0f;
        soundManager.Instance.PlaySFX("A4Block");
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
        print("Resetting Area 4's Second Puzzle");
        for (int i = 0; i < movablePieces.Length; i++)
            movablePieces[i].localPosition = startPositions[i];
        hasMovedBlocks = false;
    } 
}
