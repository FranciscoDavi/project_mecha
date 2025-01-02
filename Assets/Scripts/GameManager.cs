using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStats stats;
    public PoolObjects pool;

    private int enemiesToPool = 20;
    private double maxEnemies = 5;
    private float spawnRate = .5f;

    private float maxEnergy = 100;

    private int level = 1;

    public bool isGameActive = false;
    public bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        stats = GameStats.Instance;
        stats.maxEnergy = this.maxEnergy;
        Cursor.visible = false;
    }


    private void Start()
    {
        //Determina a quantidade de inimigos inicial
        pool.InitPool(enemiesToPool);

        InvokeRepeating("SpawnEnemies", spawnRate, 1);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
        
            PauseGame();
        }
    }

    public void SpawnEnemies()
    {
        pool.SetLimitToReturn(maxEnemies); 
        GameObject enemy = pool.GetObject();

        if (enemy != null)
        {
            enemy.SetActive(true);
        }
    }

    public void GainEnergy(float energy)
    {
        GameStats.Instance.currentEnergy += energy;
        if(GameStats.Instance.currentEnergy >= GameStats.Instance.maxEnergy)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        maxEnemies = Math.Round(maxEnemies * 1.5);
        GameStats.Instance.maxEnergy = GameStats.Instance.maxEnergy * 2;
        GameStats.Instance.currentEnergy = 0;
    }


    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else{
            Time.timeScale = 1;
            Cursor.visible = false;
        }
            
    }
    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
