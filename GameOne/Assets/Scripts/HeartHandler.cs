using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHandler : MonoBehaviour
{
    private GameManager _GameManager;
    public ParticleSystem DestroyParticle;
    public int HP;
    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyParticle.transform.parent = null;
        DestroyParticle.Play();
        Destroy(gameObject);
        _GameManager.AddHP(HP);
    }
}
