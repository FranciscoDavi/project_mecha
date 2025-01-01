using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolObjects pool;

    private int enemiesToPool = 20;
    private double maxEnemies = 5;
    private float spawnRate = .5f;

    private float currentExperience = 0;
    private double maxExperience = 50;
    private int level = 1;

    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        //Determina a quantidade de inimigos inicial
        pool.InitPool(enemiesToPool);

        InvokeRepeating("SpawnEnemies", spawnRate, 1);
       
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

    public void GainExperience(float experience)
    {   
        currentExperience += experience;
        if(currentExperience > maxExperience)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        maxEnemies = Math.Round(maxEnemies * 1.5);
        maxExperience = Math.Round(maxExperience * 1.6);

        Debug.Log($"Level: {level} \n MaxEnemies: {maxEnemies} \n Experience: {currentExperience} \n MaxExperience: {maxExperience}");
    }


}
