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
                actor.transform.position += new Vector3(Input.GetAxis("Horizontal") * 5 * Time.unscaledDeltaTime, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0,Mathf.Atan2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))*Mathf.Rad2Deg,0);
                actor.transform.rotation=Quaternion.Slerp(actor.transform.rotation,targetRotation,Time.unscaledDeltaTime*5);
            }
            if(Input.GetAxis("Vertical")!=0){
                actor.transform.position += new Vector3(0, 0, Input.GetAxis("Vertical") * 5 * Time.unscaledDeltaTime);
                Quaternion targetRotation = Quaternion.Euler(0,Mathf.Atan2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))*Mathf.Rad2Deg,0);
                actor.transform.rotation=Quaternion.Slerp(actor.transform.rotation,targetRotation,Time.unscaledDeltaTime*5);
            }

            Vector2 input = pi.actions["Move"].ReadValue<Vector2>();
            if(input.x!=0){
                actor.transform.position += new Vector3(input.x * 5 * Time.unscaledDeltaTime, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0,Mathf.Atan2(input.x,input.y)*Mathf.Rad2Deg,0);
                actor.transform.rotation=Quaternion.Slerp(actor.transform.rotation,targetRotation,Time.unscaledDeltaTime*5);
                
            }
            if(input.y!=0){
                actor.transform.position += new Vector3(0,0,input.y * 5 * Time.unscaledDeltaTime);
                Quaternion targetRotation = Quaternion.Euler(0,Mathf.Atan2(input.x,input.y)*Mathf.Rad2Deg,0);
                actor.transform.rotation=Quaternion.Slerp(actor.transform.rotation,targetRotation,Time.unscaledDeltaTime*5);
            }
        }
       
    }
}
