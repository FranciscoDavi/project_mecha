using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform target;
    protected float speed = 5f;
    protected float currentLife = 3f;
    protected float maxLife;
    public int damage = 10;
    protected float experience = 10f;

    protected Vector3 relativePosition;
    protected Quaternion targetRotation;

    protected Vector3 knockbackVelocity;
    protected float knockbackDecay = 10f; //quanto mais alto, mais o knockback "desacelera"
    protected int auxDir = 1; //usada para trocar a direção de spawn dos inimigos

    private void Awake()
    {
        
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        maxLife = currentLife;
    }

    private void OnEnable()
    {
        transform.position = RandomPositionSpawn();
    }
   
    private void Update()
    {
        //Obteim a força aplicada ao objeto e caso seja significativa suaviza o knockback.
        if (knockbackVelocity.magnitude > 0.1f) 
        {
            transform.position += knockbackVelocity * Time.deltaTime;
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    public int GetDamage()
    {
        return damage;
    }

    protected virtual void MoveToPlayer()
    {
        /*Verifica se existe um alvo, caso exista pega a posição dele e utiliza o MoveTowards para
          ir em direção ao alvo, em seguida, ajusta a rotação do inimigo para que sempre fique de frente para o jogador*/
        if (target != null && target.gameObject.activeInHierarchy)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            relativePosition = (target.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
        }
    }
    //Volta o inimigo um pouco para tras com o impacto 
    public void Knockback(float force)
    {
        Vector3 knockbackDirection = (transform.position - target.position).normalized;
        knockbackVelocity = knockbackDirection * force;
    }

    //Diminui a vida quando leva dano e caso zere desativa o objeto
    public void TakeDamage(int dmg)
    {
       currentLife -= dmg;
       if(currentLife <= 0) 
       {
            Die();
       }
    }

    public Vector3 RandomPositionSpawn()
    {
        //Retorna uma posição fora  da camera
        auxDir *= -1;
        float randomx = Random.Range(1.1f, 1.8f) * auxDir;
        float randomz = Random.Range(1.1f, 1.8f) * auxDir;

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(randomx, randomz, 20f));
        return pos;
    }


    public void Die()
    {
        GameManager.Instance.GainEnergy(experience);
        gameObject.SetActive(false);
        currentLife = maxLife;
    }

}
