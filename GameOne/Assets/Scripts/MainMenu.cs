using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Lvl1()
    {
        SceneManager.LoadScene(1);
    }
    public void Lvl2()
    {
        SceneManager.LoadScene(2);
    }
    public void Lvl3()
    {
        SceneManager.LoadScene(3);
    }
    public void Main()
    {
        SceneManager.LoadScene(0);
    }
}
