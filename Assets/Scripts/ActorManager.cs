using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    bool dead = false;
    private Animator anim;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
            anim.Play("IdleNormal");
        
        if(!dead &&(transform.position.y<-20 || transform.position.y>3)){
            Die();
        }

    }

    void Die(){
        dead=true;
        Instantiate(Resources.Load("Explosion"),transform.position,Quaternion.identity);
        audioData.Play();
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(DestroyAfter(0.5f));
    }

    IEnumerator DestroyAfter(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.tag=="Spike"){
            if(collision.gameObject.GetComponent<Spike>().deadly){
                Die();
            }
        }
        UIController.Instance.logsQueue.Enqueue(gameObject.name+ " collided with " + collision.gameObject.name);
    }
}
