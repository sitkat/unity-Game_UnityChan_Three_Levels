using UnityEngine;

public class CoinHandler : MonoBehaviour
{

    private GameManager _GameManager;
    public ParticleSystem DestroyParticle;
    public int ScoreCount;
    public ParticleSystem LoopParticle;
    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        LoopParticle.Stop();
        DestroyParticle.transform.parent = null;
        DestroyParticle.Play();
        Destroy(gameObject);
        _GameManager.AddScore(ScoreCount);
        _GameManager.CheckWinLvl1();
    }
}
