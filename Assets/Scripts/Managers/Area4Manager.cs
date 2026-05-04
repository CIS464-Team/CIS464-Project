using UnityEngine;
using System.IO;

public class Area4Manager : MonoBehaviour
{
    public static Area4Manager Instance { get; private set; }
    public bool[] LaserGoalsHit {get; set; } = new bool[4];
    public bool TunnelOpen {get; set; } = false;
    void Awake() 
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        string path = Path.Combine(Application.persistentDataPath, "saveData.json");
        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            LaserGoalsHit = data.laserGoalsHit;
            TunnelOpen = data.tunnelOpen;
        } 
    }
}