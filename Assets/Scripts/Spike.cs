using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool deadly = true;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(SpikeOnOff());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpikeOnOff()
    {
        while (true)
        {
            anim.SetInteger("state", 1);
            deadly = true;
            yield return new WaitForSeconds(5f);
            anim.SetInteger("state", 0);
            deadly = false;
            yield return new WaitForSeconds(5f);
        }
    }
}
