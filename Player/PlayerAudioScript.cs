//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip walk_Audio_Clip;
    [SerializeField] private AudioClip jump_Audio_Clip;
    [SerializeField] private AudioClip damage_Audio_Clip;
    [SerializeField] private AudioClip death_Audio_Clip;
    [SerializeField] private AudioClip win_Audio_Clip;
    [SerializeField] private AudioSource audio_Source;
    [SerializeField] private AudioSource death_Audio_Source;
    [HideInInspector] public float volume_Max,volume_Min;
    private float accumalatedDistance;
    [HideInInspector] public float step_Distance;
    private CharacterController character_Controller;
    private float jumpTimer = 0f;

    void Awake()
    {
        audio_Source = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }
    void Update()
    {
        PlayerAudio();
    }
    public void PlayerAudio(){

        if(character_Controller.isGrounded && character_Controller.velocity.sqrMagnitude > 0){

            if(Time.time >= accumalatedDistance){           
                audio_Source.clip = walk_Audio_Clip; 
                audio_Source.volume = Random.Range(volume_Min,volume_Max);
                if(!audio_Source.isPlaying){
                    audio_Source.Play();
                }
                accumalatedDistance = Time.time + step_Distance;
            }
              
        }
        if(GetComponentInParent<PlayerMovement>().jump){
             jumpTimer = Time.time + 1f;
             audio_Source.clip = jump_Audio_Clip;
             audio_Source.volume = 0.5f;
             if(!audio_Source.isPlaying){
                    audio_Source.Play();
             }
             GetComponentInParent<PlayerMovement>().jump = false;            
        }
    }
    public void DamageSound(){
        audio_Source.clip = damage_Audio_Clip;
        audio_Source.Play();
    }
    public void DeathSound(){
        death_Audio_Source.clip = death_Audio_Clip;
        death_Audio_Source.Play();
    }
    public void WinSound(){
        death_Audio_Source.clip = win_Audio_Clip;
        if(!death_Audio_Source.isPlaying){
            death_Audio_Source.Play();
        }    
    }
}
