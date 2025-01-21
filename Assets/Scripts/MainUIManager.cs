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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
        if(GameStats.Instance.isPaused == false)
        {
            crossHair.transform.position = Input.mousePosition;
        }
          
    }

    public void PauseGame()
    {
        GameStats.Instance.isPaused = !GameStats.Instance.isPaused;

        if (GameStats.Instance.isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
        }

    }















}
