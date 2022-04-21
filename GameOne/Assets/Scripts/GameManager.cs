using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text Score;
    public Slider HealthBar;
    public Image DeathScreen;
    public Image MissionCompleteScreen;
    private int TotalScore;
    public int Health = 100;
    public static GameManager ManagerInstance;
    public GameObject Player;

    private void Awake()
    {
        ManagerInstance = this;
    }
    public void DamagePlayer(int Count)
    {
        if (Health > 0)
        {
            Health -= Count;
            HealthBar.value = Health;
            Debug.Log("Вам нанесли уров в размере " + Count);
        }
        else if (Health <= 0) Death();
    }

    public void Death()
    {
        DeathScreen.gameObject.SetActive(true);
    }

    public void MissionComplete()
    {
        MissionCompleteScreen.gameObject.SetActive(true);
        Invoke("LoadSceen", 3f);
    }

    private void LoadSceen()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddScore(int Count)
    {
        TotalScore += Count;
        Score.text = "Score: " + TotalScore;
    }
    public void CheckWinLvl1()
    {
        if (TotalScore == 100)
            MissionComplete();
    }
    public void CheckWinLvl2()
    {
        if (TotalScore == 5)
            MissionComplete();
    }
    public void CheckWinLvl3()
    {
        if (TotalScore == 120)
            MissionComplete();
    }
    public void AddHP(int Count)
    {
        Health += Count;
        if (Health == 100)
        { }
        if (Health > 100)
            Health = 100;
        Debug.Log(Health);
        HealthBar.value = Health;
    }
}
