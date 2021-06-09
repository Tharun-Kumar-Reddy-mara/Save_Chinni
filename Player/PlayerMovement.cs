//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private float vertical_Velocity;
    public float gravity = 20f;
    public float jump_Height = 10f;
    private CharacterController character_Controller;
    public float speed = 3f;
    [HideInInspector] public bool jump = false;
    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        PlayerMotion();
    }
    private void PlayerMotion(){

        movement = new Vector3(SimpleInput.GetAxis("Horizontal"),0,SimpleInput.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement *= speed*Time.deltaTime;
        ApplyGravity();
        character_Controller.Move(movement);

    }
    private void ApplyGravity(){
        vertical_Velocity -= gravity*Time.deltaTime;
        movement.y = vertical_Velocity*Time.deltaTime; 
        if(vertical_Velocity < 0 && character_Controller.isGrounded){
             vertical_Velocity = 0;
        }
    }
    //For joyStick
    public void Jump(){
        if(character_Controller.isGrounded){
            vertical_Velocity = jump_Height;
            jump = true;
        }else{
            jump = false;
        }
    }
    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void ExitGame(){
       SceneManager.LoadScene("Main");
   }

}
