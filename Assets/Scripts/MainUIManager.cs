using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    public TextMeshProUGUI energyTxt;
    public TextMeshProUGUI healthTxt;

    public Slider healthBar;
    public Slider energyBar;

    public GameObject crossHair;


    private void Start()
    {
        crossHair.SetActive(true);
    }

    private void Update()
    {
        //Mira do jogador
        Aim();

        //Controla as barras de vida e energia 
        SetMaxHealth();
        SetCurrentHealth();

        //Texto somente para debug
        healthTxt.SetText($"{GameStats.Instance.currentHealth} - {GameStats.Instance.maxHealth}") ;
        energyTxt.SetText($"{GameStats.Instance.currentEnergy} - {GameStats.Instance.maxEnergy}");
    }

    private void SetMaxHealth()
    {
        healthBar.maxValue = GameStats.Instance.maxHealth;
        energyBar.maxValue = GameStats.Instance.maxEnergy;
    }

    private void SetCurrentHealth()
    {
        healthBar.value = GameStats.Instance.currentHealth;
        energyBar.value = GameStats.Instance.currentEnergy;

    }


    private void Aim()
    {
        if(GameManager.Instance.isPaused == false)
        {
            crossHair.transform.position = Input.mousePosition;
        }
          
    }















}
