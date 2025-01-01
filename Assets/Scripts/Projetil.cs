using System.Collections;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    private Rigidbody rb;
    private int damage;

    //Inicia referencias e variaveis
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        damage = 1;
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
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.Knockback(15);
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
        gameObject.SetActive(false);
    }


}
