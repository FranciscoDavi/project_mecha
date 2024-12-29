using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rgb;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    private float life = 5;

    private float experience = 10f; 
    
    private Vector3 relativePosition;
    private Quaternion targetRotation;

    private Vector3 knockbackVelocity;
    [SerializeField] private float knockbackForce = 50f;
    private float knockbackDecay = 10f; //quanto mais alto, mais o knockback "desacelera"

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

        }
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
        /*Verifica se existe um alvo, caso exista pega a posição dele e utiliza o MoveTowards para
            ir em direção ao alvo, em seguida, ajusta a rotação do inimigo para que sempre fique de frente para o jogador*/
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            relativePosition = (target.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
        }   
    }

    //Volta o inimigo um pouco para tras com o impacto 
    public void Knockback()
    {
        Vector3 knockbackDirection = (transform.position - target.position).normalized;
        knockbackVelocity = knockbackDirection * knockbackForce;
    }

    //Diminui a vida quando leva dano e caso zere desativa o objeto
    public void TakeDamage(int dmg)
    {
       life -= dmg;
       if(life <=0) 
       {
            GameManager.Instance.SetExperience(experience);
            gameObject.SetActive(false);
            life = 5;
       }
    }
}
