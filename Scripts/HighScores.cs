using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScores : MonoBehaviour
{
    public Text highScores;
    public static int num_scores = 10;

    public void ShowTopScores()
    {
        string path = "Assets/Resources/StoreScores.txt";
        string line;
        string[] fields;
        string[] playerNames = new string[num_scores];
        int[] playerScores = new int[num_scores];
        int scores_read = 0;

        highScores.text = "";
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream && scores_read < num_scores)
        {
            line = reader.ReadLine();
            fields = line.Split(',');
            highScores.text += fields[0] + " ; " + fields[1] + "\n";
            scores_read += 1;
        }
    }
    void Start()
    {
        ShowTopScores();
    }
}
