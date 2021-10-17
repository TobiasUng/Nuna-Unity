using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] public static PilotStats pilotStats = new PilotStats();
    public static string fileName;
    public static float startTime;
    public static float endTime;
    public static string participant = "P14";


    public void Start()
    {
        /*fileName = SceneManager.GetActiveScene().name + "_" + System.DateTime.Now + ".json";
        fileName = fileName.Replace(" ", "_");*/
        pilotStats.participant = participant;
        pilotStats.scene = SceneManager.GetActiveScene().name;
        fileName = SceneManager.GetActiveScene().name + " " + System.DateTime.Now + ".json";
        fileName = fileName.Replace("/", "_");
        fileName = fileName.Replace(":", ".");
    }

    public void Update()
    {
        
    }

    public static void saveToJson()
    {
       
        string statsJson = JsonUtility.ToJson(pilotStats);
        Debug.Log(Application.dataPath);
        System.IO.File.WriteAllText(Application.dataPath + "/StudyResults/" + fileName, statsJson);

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Exit");
        saveToJson();
    }


    
}

[System.Serializable]
public class PilotStats
{
    public string participant = "";
    public string scene = "";
    public float robotIdleTime = 0f;
    public float distanceMoved = 0f;
    public float avarageScrewDistance = 0f;
    public float completionTime = 0f;
    public List<float> screwResponse = new List<float>();
    //public List<float> screwResponseAlt = new List<float>();
    public float responseTime = 0f;
    public int dangerErrors = 0;
    public int errors = 0;
    public int screwsPlaced = 0;
    public int nutsPlaced = 0;
    public List<string> feedbacks = new List<string>();

}


