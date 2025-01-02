using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI energyTxt;
    public GameObject crossHair;


    private void Start()
    {
        crossHair.SetActive(true);
    }

    private void Update()
    {
        
        Aim();
        healthTxt.SetText($"{GameStats.Instance.currentHealth} / {GameStats.Instance.maxHealth}");
        energyTxt.SetText($"{GameStats.Instance.currentEnergy} / {GameStats.Instance.maxEnergy} ");
    }

    private void Aim()
    {
        if(GameManager.Instance.isPaused == false)
        {
            crossHair.transform.position = Input.mousePosition;
        }
          
    }















}
