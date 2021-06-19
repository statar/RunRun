/*
 using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public static GameDataScript instance;
    public int bestScoreData;
    private void Awake()
    {
        instance = this;
    }

    public void SaveLevelDataAsJson()
    {
        string path = Application.dataPath + "/Resources/" + "RunRunGameData" + ".json";
        var data = SerializeMapData();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(data);
            }
        }
        AssetDatabase.Refresh();
    }

    private string SerializeMapData()
    {
        var levelData = new LevelData {bestScore = CanvasController.instance.bestScore};
        var data = JsonUtility.ToJson(levelData);
        return data;
    }

    public void LoadLevelDataFromJson()
    {
        string path = Application.dataPath + "/Resources/" + "RunRunGameData";
        var data = ReadDataFromText(path);
        var levelData = JsonUtility.FromJson<LevelData>(data);
        LoadScene(levelData);
    }

    private string ReadDataFromText(string path)
    {
        string data = null;
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    data = reader.ReadToEnd();
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
        return data;
    }

    private void LoadScene(LevelData levelData)
    {
        var score = levelData.bestScore;
        bestScoreData = score;
    }

}

[Serializable]
public class LevelData
{
    public int bestScore;
}
*/
