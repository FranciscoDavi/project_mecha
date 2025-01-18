using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public GameObject projetilPrefab;
    public float speedProjetil = 50f;
    public float distObj;
    public float minDist = 10f;
  
    public float TimeToAttack = 1f;

    private void Update()
    {

        distObj = Vector3.Distance(transform.position, target.transform.position);
        Debug.Log(distObj);

        
        Debug.Log(TimeToAttack);
        if (distObj <= minDist) 
        {
            if(TimeToAttack > 0)
                TimeToAttack -= 1 * Time.deltaTime;

            if (TimeToAttack <= 0) {
                Attack();
                TimeToAttack = 1f;
            }
        }
    }
    protected override void MoveToPlayer()
    {
        /*Verifica se existe um alvo, caso exista pega a posição dele e utiliza o MoveTowards para
          ir em direção ao alvo, em seguida, ajusta a rotação do inimigo para que sempre fique de frente para o jogador*/
        if (target != null && target.gameObject.activeInHierarchy && distObj >= minDist)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            relativePosition = (target.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
        }
    }

    public void Attack()
    {
        relativePosition = (target.position - transform.position).normalized;
        GameObject projetil = Instantiate(projetilPrefab, transform.position, Quaternion.identity);
        projetil.GetComponent<Rigidbody>().velocity = relativePosition * speedProjetil;
     

    }


}
