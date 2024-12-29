using TMPro;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] public int totalLife ;
    private int currentLife;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] private float timeToTakeDamage;
    private float lastDamageTime;

    private void Awake()
    {
        totalLife = 3;
        currentLife = 0;
        timeToTakeDamage = 0;
        lastDamageTime = 0;
        currentLife = totalLife;
    }

    private void TakeDamage(int dmg)
    {
        currentLife -= dmg;
        if (currentLife <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Time.time > lastDamageTime + timeToTakeDamage)
        {
            TakeDamage(1);
            lastDamageTime = Time.time;

            int enemyLayer = LayerMask.GetMask("Enemies");
            Collider[] hitColliders =  Physics.OverlapSphere(transform.position, 5, enemyLayer);
            
            foreach (Collider collider in hitColliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.Knockback(); 
            }   
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

   
}
