using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead &&(transform.position.y<-20 || transform.position.y>3)){
            dead=true;
            Instantiate(Resources.Load("Explosion"),transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        UIController.Instance.logsQueue.Enqueue(gameObject.name+ " collided with " + collision.gameObject.name);
    }
}
