using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStats Stats;
    public List<GameObject> EnemiesPrefab = new List<GameObject>();
    public List<PoolObjects> PoolEnemies = new List<PoolObjects>();

    private int EnemiesToPool = 20;
    public double maxEnemies = 2;
    public float spawnRate = .5f;

    public float maxEnergy = 100;

    public int level = 1;

    public bool isGameActive = false;
   

    private void Awake()
    {
        Instance = this;
        Stats = GameStats.Instance;
        Stats.maxEnergy = maxEnergy;
        Cursor.visible = false;
        isGameActive = true;
    }


    private void Start()
    {
        //Determina a quantidade de inimigos inicial
        PoolEnemies[0].InitPool(EnemiesPrefab[0], EnemiesToPool, 5);
        InvokeRepeating("SpawnEnemies", spawnRate, 1);
       
    }

    public void SpawnEnemies()
    {
        PoolEnemies[0].SetLimitToReturn(maxEnemies); 
        GameObject enemy = PoolEnemies[0].GetObject();

        if (enemy != null)
        {
            enemy.SetActive(true);
        }

        if (level > 1)
        {
            PoolEnemies[1].SetLimitToReturn(maxEnemies - Convert.ToInt32(maxEnemies * .5));
            GameObject enemy2 = PoolEnemies[1].GetObject();

            if (enemy2 != null) {

                enemy2.SetActive(true);
            }  
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
        maxEnemies = Math.Round(maxEnemies * 1.5f);
        GameStats.Instance.maxEnergy = Convert.ToInt64(GameStats.Instance.maxEnergy * 1.6f);
        GameStats.Instance.currentEnergy = 0;

        if(level >= 2)
        {
            PoolEnemies[1].InitPool(EnemiesPrefab[1], 10, 5);
        }    

    }
   
    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over!");
    }
}
