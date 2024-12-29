using UnityEngine;
using UnityEngine.InputSystem;

public class ShotPlayer : MonoBehaviour
{
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private PoolObjects shotPool;
    [SerializeField] private Vector3 shotPoint;
    [SerializeField] private float projectileSpeed = 50f;
    private float lastShotTime;
    [SerializeField] private float fireCooldown = 0.5f;


    //Iniciando varias e referencias
    private void Awake()
    {
        shotPoint = transform.position;
    }
    private void Update()
    {
        //Definindo a posição, deve ser melhorado posteriormente quando adicionar assets
        shotPoint = transform.position;
    }

    private void FixedUpdate()
    {
        //Verifica a posição do mouse e rotaciona o jogador em direção a ele
        Vector3 aimPos = GetMousePosition();
        Vector3 relativePosition = (aimPos - transform.position).normalized;
        relativePosition.y = 0;

        Quaternion aimRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRotation, Time.deltaTime * 5);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //Verfica se ação de clicar foi concluida
        if (context.performed)
        {    
            //Verifica se pode realizar um novo tiro
            if (Time.time > lastShotTime + fireCooldown)
            {
                //Determina a direção do tiro com base na posição do mouse
                Vector3 shotDirection = (GetMousePosition() - transform.position).normalized;
                shotDirection.y = 0;

                GameObject projectile = shotPool.GetObject();
                
                if (projectile != null)
                {
                    projectile.transform.position = transform.position;
                    projectile.SetActive(true);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.velocity = shotDirection * projectileSpeed;
                    }

                    lastShotTime = Time.time;
                }
            }
        }
    }
    //Cria um plano com os vetores X e Z e um Raycast, caso atinja esse plano, retorne a posição 
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); 
        
        float distance;

        if (plane.Raycast(ray, out distance)) {
            Vector3 targetPoint = ray.GetPoint(distance);
            return targetPoint;
        }
    
        return Vector3.zero;
    }
     
     
 
}
