using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform objPlayer;
    [SerializeField] private Vector3 offsetCam;
    [SerializeField] private float smoothSpeed;

    private void Awake()
    {
        smoothSpeed = 0.8f;
    }

    private void Start()
    {
        if(objPlayer != null)
        {
            objPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        if (objPlayer != null)
        {
            Vector3 desiredPosition = objPlayer.position + offsetCam;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }
}
