using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EnemiesPrefab;
    public GameStats Stats;
    public PoolObjects PoolEnemies;

    private int EnemiesToPool = 20;
    private double maxEnemies = 5;
    private float spawnRate = .5f;

    private float maxEnergy = 100;

    private int level = 1;

    public bool isGameActive = false;
   

    private void Awake()
    {
        Instance = this;
        Stats = GameStats.Instance;
        Stats.maxEnergy = this.maxEnergy;
        Cursor.visible = false;
    }


    private void Start()
    {
        //Determina a quantidade de inimigos inicial
        PoolEnemies.InitPool(EnemiesPrefab, EnemiesToPool, 5);
        InvokeRepeating("SpawnEnemies", spawnRate, 1);
       
    }

    public void SpawnEnemies()
    {
        PoolEnemies.SetLimitToReturn(maxEnemies); 
        GameObject enemy = PoolEnemies.GetObject();

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
   
    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
