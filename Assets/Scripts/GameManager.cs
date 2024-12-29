using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI levelText;
    public static GameManager Instance;
    public PoolObjects pool;
    private float spawnRate;
    private int auxDir = 1; //usada para trocar a direção de spawn dos inimigos

    private float currentExperiencie = 0;
    private float maxExperiencie = 30;

    private int level = 1;

    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1, 1);
        levelText.SetText($"{level}");
    }

    public void SpawnEnemies()
    {
        GameObject enemy = pool.GetObject();
        if (enemy != null)
        {
            enemy.transform.position = RandomPositionSpawn();
            enemy.SetActive(true);

        }
    }

    public Vector3 RandomPositionSpawn()
    {
        auxDir *= -1;
        float random = Random.Range(1.1f, 1.8f) * auxDir;

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(random, random, 20f));
        return pos;
    }

    public void SetExperience(float experience)
    {
       
        currentExperiencie += experience;
        if(currentExperiencie > maxExperiencie)
        {
            level++;
            maxExperiencie += 30;
        }
        levelText.SetText($"{level}");
    }

   
}
