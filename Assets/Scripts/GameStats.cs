using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public float currentHealth;
    public float maxHealth;
    public float moveSpeed;
    public float currentEnergy;
    public float maxEnergy;

    public int level;
  
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

}
