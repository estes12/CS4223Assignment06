using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public static int lives = 3;
    private int death;
    public int currentLives;
    public Text lifeDisplay;

    public static int score;
    public Text scoreText;

    public GameObject pickUp;
    public Transform[] spawnPoints;

    void Start()
    {
        agent.updateRotation = false;
        currentLives = lives;
        DisplayLives();
        score = 0;
        SetCountText();
    }

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
        character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        
    }

    public void RandomSpawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(pickUp, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score = score + 100;
            SetCountText();
            RandomSpawn();
        }
        
        death = lives;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You lost a life!");
            death--;
        }
        else {
            int num_scores = 10;
            string path = "Assets/Resources/StoreScores.txt";
            string line;
            string[] fields;
            int scores_written = 0;
            string newName = MainMenu.input;
            string newScore = score.ToString();
            bool newScoreWritten = false;
            string[] writeNames = new string[10];
            string[] writeScores = new string[10];

            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                fields = line.Split(',');
                if (!newScoreWritten && scores_written < num_scores) // if new score has not been written yet
                {
                    //check if we need to write new higher score first
                    if (Convert.ToInt32(newScore) > Convert.ToInt32(fields[1]))
                    {
                        writeNames[scores_written] = newName;
                        writeScores[scores_written] = newScore;
                        newScoreWritten = true;
                        scores_written += 1;
                    }

                }
                if (scores_written < num_scores) // we have not written enough lines yet
                {
                    writeNames[scores_written] = fields[0];
                    writeScores[scores_written] = fields[1];
                    scores_written += 1;
                }
            }
            reader.Close();

            // now we have parallel arrays with names and scores to write
            StreamWriter writer = new StreamWriter(path);

            for (int x = 0; x < scores_written; x++)
            {
                writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
            }
            writer.Close();

            AssetDatabase.ImportAsset(path);
            TextAsset asset = (TextAsset)Resources.Load("StoreScores");

        }
        lives = death;
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
            Debug.Log("You lost the game!");
        }
        if (score >= 1000)
        {
            SceneManager.LoadScene(2);
            Debug.Log("You have won the game!");
        }
    }

    public void DisplayLives()
    {
        lifeDisplay.text = "Lives: " + currentLives.ToString();
    }
}
