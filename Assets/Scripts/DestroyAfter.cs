using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    // Start is called before the first frame update

    public float delay = 1;
    void Start()
    {
        StartCoroutine(DestroyAfterSeconds(delay));
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
