using UnityEngine;
using UnityEngine.Tilemaps;

// Attached to the player, updates the manager on which tile the player is on
public class Area2PuzzleSensor : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Area2Manager manager;
    
    private Vector3Int lastCell;

    void Update()
    {
        // Find cell position based on player world position
        Vector3Int currentCell = floorTilemap.WorldToCell(transform.position);

        // If player has moved to a new tile
        if (currentCell != lastCell)
        {
            lastCell = currentCell;
            
            // Check this tile
            manager.CheckTile(currentCell);
        }
    }
}