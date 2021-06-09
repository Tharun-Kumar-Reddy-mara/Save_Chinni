//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TowerDefenceSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackClip,deadClip;
    public void AttackSound(){
        audioSource.clip = attackClip;
        if(!audioSource.isPlaying){
            audioSource.Play();
        }       
    }
    public void DeadSound(){
        audioSource.clip = deadClip;
        audioSource.Play();
    }
}
