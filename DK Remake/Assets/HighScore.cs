using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public string filename = "highscores.json";
    public int maxScores = 4;
    public string fullpath
    {
        get
        {
            if (filename.EndsWith(".json"))
                return Application.persistentDataPath + filename;
            else
                return Application.persistentDataPath + filename + ".json";
        }
    }
    Text text;

    public class Score
    {
        public string name;
        public int score;
        public Score(string n, int s)
        {
            name = n;
            score = s;
        }
    }
    public class Data
    {
        public List<Score> scores;
    }
    Data data;

    void Start()
    {
        Load();
        text = GetComponent<Text>();
        if (!text)
        {
            Debug.LogError("HighScore must be attached to an object with Text!");
        }
    }

    void Update()
    {
        if (text)
        {
            text.text = "";
            for (int i = 0; i < data.scores.Count && i < maxScores; ++i)
            {
                Score s = data.scores[i];
                text.text += s.name + "\t" + s.score + "\n";
            }
        }
    }

    public Score GetScore(int i)
    {
        return data.scores[i];
    }

    public void AddScore(string name, int score)
    {
        AddScore(new Score(name, score));
    }

    public void AddScore(Score score)
    {
        bool added = false;
        for (int i = 0; i < data.scores.Count; ++i)
            if (GetScore(i).score < score.score)
            {
                data.scores.Insert(i, score);
                added = true;
                break;
            }
        if (!added)
            data.scores.Add(score);
    }

    public void Save()
    {
        StreamWriter sw = new StreamWriter(fullpath);
        sw.Write(JsonUtility.ToJson(data));
        sw.Close();
    }

    public void Load()
    {
        StreamReader sr = new StreamReader(fullpath);
        if (sr == null)
        {
            Debug.LogError("Couldn't load path: " + fullpath);
            return;
        }
        data = JsonUtility.FromJson<Data>(sr.ReadToEnd());
        sr.Close();
    }
}
