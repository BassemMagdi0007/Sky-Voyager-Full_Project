using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject boss;
    public Transform spawnBoss;
    private GameObject hazard;
    
    public Vector3 spawnValues;
    public int hazardCount;
    public int BossSummoning;
    public float spawnWait;
    float startWait = 10;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text win;
    public Text Pause;


    public static bool BoosDefeated;
    private bool gameOver;
    private bool restart;
    private bool BossisCreated;
    private int score;

    AudioSource audioSource;

    void Start()
    {

        Screen.SetResolution(550, 700, true);
        
        gameOver = false;
        restart = false;
        BoosDefeated = false;
        restartText.text = "";
        gameOverText.text = "";
        win.text = "";
        Pause.text = "";
        
        score = 0;
        audioSource = GetComponent<AudioSource>();
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0;
            Pause.text = "Pause";
            audioSource.mute = true;

        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Time.timeScale = 1;
            Pause.text = "";
            audioSource.mute = false;
        }

        Win();

        if (Input.GetKey("escape"))
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    if (!BossisCreated)
                    {
                        Instantiate(hazard, spawnPosition, spawnRotation);
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
            yield return new WaitForSeconds(waveWait);
        }
      
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if(score >= BossSummoning)
        {
            for(int j = 0; j< hazards.Length ; j++)
            {
                hazards[j] = null;   
            }
            if (!BossisCreated)
            {
                Instantiate(boss, spawnBoss.position, Quaternion.identity);
                BossisCreated= true;
            } 
        }

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true; 

        restartText.text = "Press 'R' To Restart";
        restart = true;
    }

    public void Win()
    {
        if(BoosDefeated == true && gameOver!=true)
        {
            win.text = "Boss Defeated";
            restartText.text = "Press 'R' To Restart";
            restart = true;
        }
    }
}
