//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum BulletType{
    BULLET,
    LASER
     
}
public enum FireType{
    SINGLE,
    MULTIPLE
}
public enum AimType{
    AIM
}
public class WeaponHandler : MonoBehaviour
{   

    private Animator anim;
    public BulletType bullet_Type;
    public FireType fire_Type;
    public AimType aim_Type;
    public int damage = 100;
    public float weapon_Range = 100f;
    [SerializeField] private AudioSource gun_Sound;
    [SerializeField] private AudioClip shoot,reload;
    [SerializeField] GameObject muzzleFlash;
    
    public Transform gun_End;
    public int maxBullets = 30;
    [HideInInspector] public int currentBullets;

    private WeaponManager weapon_Manager;
    public bool stopFiring;
    public int maxAmmo = 30;
    [HideInInspector] public int currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentBullets = maxBullets;
    }

    private void Awake()
    {
        weapon_Manager = GetComponentInParent<WeaponManager>();
        anim = GetComponent<Animator>();
    }

    public void MuzzleFlash(){
        GameObject effect = Instantiate(muzzleFlash,gun_End);
        Destroy(effect,0.1f);
    } 
    public void WeaponSoundPlay(){ 
          gun_Sound.clip = shoot;      
          gun_Sound.Play();
    }
    public void WeaponSoundStop(){       
          gun_Sound.Stop();
    }
    public void ReloadSoundPlay(){
        gun_Sound.clip = reload;
        gun_Sound.Play();
    }
    public void BulletsDecrement(int bullets_Used){
        currentBullets -= bullets_Used;
        if(currentBullets <= 0){
              currentBullets = 0;
              WeaponSoundStop();
        }
    }
    public void StartShootAnimation(){
        anim.SetBool("Shoot",true);
    }
    public void StopShootAnimation(){
        anim.SetBool("Shoot",false);
    }
    public void StartReloadAnimation(){
        anim.SetBool("Reload",true);
    }
    public void StopReloadAnimation(){
        anim.SetBool("Reload",false);
    }
}
