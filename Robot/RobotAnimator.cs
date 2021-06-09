//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RobotAnimator : MonoBehaviour
{
     private Animator anim;
    void Awake(){
       anim = GetComponent<Animator>();
    }
    public void Walk(bool walk){
        anim.SetBool("Walk",walk);
    }
    public void Run(bool run){
        anim.SetBool("Run",run);
    } 
    public void Attack(bool attack){
        anim.SetBool("Attack",attack);
    }
    public void Jump(){
        anim.SetTrigger("Jump");
    }
    public void Die(){
        anim.SetTrigger("Die");
    }
}
