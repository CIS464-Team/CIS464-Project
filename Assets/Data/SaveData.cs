using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string currentArea;
    public bool[] keysHeld = new bool[4];
    public bool[] laserGoalsHit = new bool[4];
    public bool tunnelOpen = false;
}
