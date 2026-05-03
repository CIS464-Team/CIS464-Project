using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Area2Manager : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap puzzleTilemap;
    public TileBase normalTile;
    public TileBase failTile;

    [Header("Game Objects")]
    public GameObject gate;
    public ResetButton resetButton;

    // Allows for new paths to be added easily if we want to
    [Header("Path Settings")]
    public List<PathData> presetPaths; 
    private int currentPathIndex = 0;
    private List<Vector3Int> triggeredTiles = new List<Vector3Int>();
    private bool isFailed = false;

    [System.Serializable]
    public class PathData { public List<Vector3Int> tiles; }

    void Start()
    {
        ResetPuzzle(); // Reset puzzle to initial state 
    }

public void CheckTile(Vector3Int pos)
{
    // If puzzle is already failed, we don't need to check anything
    if (isFailed) return;

    // If the tile doesn't exist within puzzle tilemap, ignore it
    if (!puzzleTilemap.HasTile(pos)) return; 

    // Check if the current tile is part of the correct path
    if (presetPaths[currentPathIndex].tiles.Contains(pos))
    {
        if (soundManager.Instance != null)
        {
            soundManager.Instance.PlaySFX("CorrectTile"); 
        }
        Debug.Log("Safe tile. Stepped on: " + pos);
    }
    else
    {
        FailPuzzle(pos);
    }
}

void FailPuzzle(Vector3Int pos)
{
    isFailed = true;
    // Change the tile to the "fail" tile
    if (puzzleTilemap != null && failTile != null)
    {
        puzzleTilemap.SetTile(pos, failTile);
        triggeredTiles.Add(pos);
        Debug.Log("Fail tile placed at: " + pos);
    }

    // Enable the gate blocking the player from getting the key
    if (gate != null) 
    {
        gate.SetActive(true);
        soundManager.Instance.PlaySFX("IncorrectTile");
        soundManager.Instance.PlaySFX("Area2Gate");
    }
    if (resetButton != null) 
    {
        resetButton.ShowButton();
    }
    else
    {
        Debug.LogError("Reset Button is not set in the Manager Inspector");
    }
}

    public void ResetPuzzle()
    {
        isFailed = false;
        
        // Change fail tile back to normal tile
        foreach (Vector3Int pos in triggeredTiles)
        {
            puzzleTilemap.SetTile(pos, normalTile);
        }
        triggeredTiles.Clear();

        gate.SetActive(false); // disable the gate
        
        // Cycle to a new path (if new have new paths to cycle through)
        currentPathIndex = (currentPathIndex + 1) % presetPaths.Count;
    }
}