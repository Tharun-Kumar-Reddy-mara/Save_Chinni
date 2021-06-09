using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillySounds : MonoBehaviour
{
    [SerializeField] private AudioSource lilly_Audio;
    [SerializeField] private AudioSource lilly_Death_Audio;

    [SerializeField] private AudioClip help_Sound,death_Sound,damage_Sound;
    private float helpSoundTimer;
    private float helpSoundForThisTime = 20f;
    private void Update()
    {   
        if(GetComponent<HealthScript>().health < 0f){
            return;
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().health <= 0f){
            return;
        }
        helpSoundTimer += Time.deltaTime; 

        if(helpSoundTimer > helpSoundForThisTime){
           HelpSound();
           helpSoundTimer = 0f;
        }
    }
    private void HelpSound(){
        lilly_Audio.clip = help_Sound;
        lilly_Audio.Play();
    }
    public void DeathSound(){
        lilly_Death_Audio.clip = death_Sound;
        lilly_Death_Audio.Play();
    }
     public void DamageSound(){
        lilly_Audio.clip = damage_Sound;
        lilly_Audio.Play();     
    }

}
