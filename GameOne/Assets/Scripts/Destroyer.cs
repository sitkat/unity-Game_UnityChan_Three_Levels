using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyObj());
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
