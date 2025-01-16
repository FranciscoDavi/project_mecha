using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public  float maxHealth = 100;
    private float timeToTakeDamage = 1;
    private float lastDamageTime;

    private void Awake()
    {
        GameStats.Instance.maxHealth = this.maxHealth;    
    }

    private void TakeDamage(int dmg)
    {
        GameStats.Instance.currentHealth -= dmg;
        if (GameStats.Instance.currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Time.time > lastDamageTime + timeToTakeDamage)
        {

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            TakeDamage(enemy.GetDamage());
            lastDamageTime = Time.time;

            int enemyLayer = LayerMask.GetMask("Enemies");
            Collider[] hitColliders =  Physics.OverlapSphere(transform.position, 5, enemyLayer);
            
            foreach (Collider collider in hitColliders)
            {
                enemy = collider.GetComponent<Enemy>();
                enemy.Knockback(50); 
            }   
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

   
   
}
