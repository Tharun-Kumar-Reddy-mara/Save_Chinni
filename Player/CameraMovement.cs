//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   private Vector2 camera_Movement;
   private Vector2 look_Angle;
   public float mouse_Sensitivity = 100f;
   public Vector2 mouse_X_Movement = new Vector2(-60,60);
  [SerializeField] private Transform player_Root,Look_Root;
   private bool invert;
    void Update()
    {   
        CameraLook(); 
    }
    private void CameraLook(){
         
         camera_Movement = new Vector2(SimpleInput.GetAxis("MouseY"),SimpleInput.GetAxis("MouseX")); 
         look_Angle.x += camera_Movement.x*mouse_Sensitivity*Time.deltaTime*(invert?1f:-1f);
         look_Angle.y += camera_Movement.y*mouse_Sensitivity*Time.deltaTime;
         look_Angle.x = Mathf.Clamp(look_Angle.x,mouse_X_Movement.x,mouse_X_Movement.y);
         Look_Root.localRotation = Quaternion.Euler(look_Angle.x,0f,0f);
         player_Root.localRotation = Quaternion.Euler(0f,look_Angle.y,0f);
        
    }
}
