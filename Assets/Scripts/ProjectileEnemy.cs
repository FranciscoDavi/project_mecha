using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private int damage;

    //Inicia referencias e variaveis
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        damage = 10;
    }

    private void Update()
    {
        //Verifica se o objeto esta ativo na tela e se sim inicia o tempo para desaparecer
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(TimeToDisppair());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Verifica se o projetil colide com o inimigo e se sim desaparece
        if (other.CompareTag("Player"))
        {
            DamagePlayer player = other.GetComponent<DamagePlayer>();
            player.TakeDamage(damage);
            Disppair();
        }
    }
    IEnumerator TimeToDisppair()
    {
        yield return new WaitForSeconds(1f);
        Disppair();
    }

    private void Disppair()
    {
        Destroy(gameObject);
    }
}
