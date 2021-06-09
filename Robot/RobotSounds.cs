//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotSounds : MonoBehaviour
{
     private  NavMeshAgent agent;
    [SerializeField] private AudioClip scare_Clip,die_Clip,attack_Clip;
    [SerializeField] private AudioSource audio_Source;
    [SerializeField] private AudioSource attck_Audio_Source;
    void Awake()
    {
       agent = GetComponent<NavMeshAgent>(); 
       audio_Source = GetComponent<AudioSource>();
    }
   
    //For all Robot sounds
     public void ScreamSound(){
        audio_Source.clip = scare_Clip;
        audio_Source.Play();
    }
    public void AttackSound(){
        attck_Audio_Source.clip = attack_Clip;
        attck_Audio_Source.Play();
    }
    public void DeathSound(){
        audio_Source.clip = die_Clip;
        audio_Source.Play();
    }
}
