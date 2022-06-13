using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public GameObject actor=null;
    PlayerInput pi;
    // Start is called before the first frame update
    void Start()
    {
        pi=GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(actor!=null){
            if(Input.GetAxis("Horizontal")!=0){
                actor.transform.Translate(Vector3.right*Input.GetAxis("Horizontal")*5 * Time.deltaTime);
            }
            if(Input.GetAxis("Vertical")!=0){
                actor.transform.Translate(Vector3.forward * Input.GetAxis("Vertical")*5 * Time.deltaTime);
            }

            Vector2 input = pi.actions["Move"].ReadValue<Vector2>();
            if(input.x!=0){
                actor.transform.Translate(Vector3.right*input.x*5 * Time.deltaTime);
            }
            if(input.y!=0){
                actor.transform.Translate(Vector3.forward * input.y*5 * Time.deltaTime);
            }
        }
       
    }
}
