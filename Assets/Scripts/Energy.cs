using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] float AmountEnergy = 10f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GainEnergy(AmountEnergy);
            Destroy(gameObject);
        }
    }




}
