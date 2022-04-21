using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.AddScore(1);
        Destroy(this.gameObject);
        GameManager.CheckWinLvl2();
    }
}
