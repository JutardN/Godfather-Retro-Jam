using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ScoreSave
{
    public static void SaveScore(int score)
    {
        ScoreData data;
        if (File.Exists(Application.persistentDataPath + "/score.save"))
        {
             data = GetScores();
        }
        else
        {
            data = new ScoreData();
        }
        data.listScore.Insert(0,score);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static ScoreData GetScores()
    {
        string path = Application.persistentDataPath + "/score.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ScoreData listScore = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            
            return listScore;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }

}
