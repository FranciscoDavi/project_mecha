using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyRanger : Enemy
{
    [SerializeField] public GameObject ProjectileEnemy;
    [SerializeField] private float SpeedProjetil = 20f;
    private float DistancePlayer;
    [SerializeField] private float AttackRange = 10f;
    [SerializeField] private float MoveSpeedWhenAttacking = .7f;
    [SerializeField] private float TimeToAttack = 1f;

    private void Update()
    {
        if (target != null) {
            DistancePlayer = Vector3.Distance(transform.position, target.transform.position);

            if (DistancePlayer <= AttackRange)
            {
                if (TimeToAttack > 0)
                    TimeToAttack -= 1 * Time.deltaTime;

                if (TimeToAttack <= 0 && GameManager.Instance.isGameActive)
                {
                    Attack();
                    TimeToAttack = 1f;
                }
            }
        }
    }
    protected override void MoveToPlayer()
    {
        if (target != null && target.gameObject.activeInHierarchy )
        {
            float step = speed * Time.deltaTime;

            if (DistancePlayer <= AttackRange)
            {
                step *= MoveSpeedWhenAttacking;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            relativePosition = (target.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
        }
    }

    public void Attack()
    {
        if(TimeToAttack > 0)
        {
            TimeToAttack-= Time.deltaTime;
        }

        if (TimeToAttack <= 0) {
            GameObject fire = Instantiate(ProjectileEnemy, transform.position, Quaternion.identity);
            Vector3 shotDirection = (target.position - transform.position ).normalized;
            fire.GetComponent<Rigidbody>().velocity = SpeedProjetil * shotDirection;
            TimeToAttack = 1f;
        }
        
    }


}
