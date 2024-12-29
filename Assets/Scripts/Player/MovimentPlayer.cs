using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentPlayer : MonoBehaviour
{
    private Rigidbody rgb;
    [SerializeField] private float movementSpeed;
    private Vector2 moveInput;

    void Start()
    {
        movementSpeed = 20f;
        rgb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {       
        Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        rgb.AddForce(dir * movementSpeed, ForceMode.Force);

        //Evita que a velocidade exceda o limite
        if (rgb.velocity.magnitude > movementSpeed)
        {
            rgb.velocity = rgb.velocity.normalized * movementSpeed;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }

    
}
