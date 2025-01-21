using UnityEngine;
using UnityEngine.InputSystem;

public class ShotPlayer : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private PoolObjects ShotPool;
    [SerializeField] private Vector3 shotPoint;
    [SerializeField] private float projectileSpeed = 50f;
    private bool isFiring = false;

    [SerializeField]  private float TimeToShot = .2f;

    private void Start()
    {
        ShotPool.InitPool(ProjectilePrefab, 10, 1);
    }

    private void Update()
    {
        //Atualiza o tempo para o proximo tiro
        if (TimeToShot > 0)
        {
            TimeToShot -= Time.deltaTime;
           
        }

        if (isFiring && TimeToShot <= 0)
        {
            //Determina a direção do tiro 
            Vector3 shotDirection = (GetMousePosition() - transform.position).normalized;
            shotDirection.y = 0;

            GameObject projectile = ShotPool.GetObject();
            
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.SetActive(true);

                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = shotDirection * projectileSpeed;
                }
            }

            TimeToShot = .2f; //tempo entre os tiros
        }
    }
    private void FixedUpdate()
    {
        lookForMouse();
    }
   
    public void OnFire(InputAction.CallbackContext context)
    {
        //Verfica se ação de clicar foi concluida
        if (context.started)
        {
            isFiring = true;
        }
        else if (context.canceled)
        {
            isFiring = false;
        }
                 
    }

    //Cria um plano com os vetores X e Z e um Raycast, caso atinja esse plano, retorne a posição 
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); 
        
        float distance;
        int layerMask = ~LayerMask.GetMask("Enemies");

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            return hit.point;
        }

        if (plane.Raycast(ray, out distance)) {
            return ray.GetPoint(distance);
        }
    
        return Vector3.zero;
    }
    

    private void lookForMouse()
    {
        //Verifica a posição do mouse e rotaciona o jogador em direção a ele
        Vector3 aimPos = GetMousePosition();
        Vector3 relativePosition = (aimPos - transform.position).normalized;
        relativePosition.y = 0;

        Quaternion aimRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRotation, Time.deltaTime * 5);
    }
     
   
 
}
