using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;
    private int damaging;
    private bool stopFiring;
    private Camera mainCam;
    private float fireRate = 15f;
    private float nextFireRate;
    private float range;
    private Transform gunEnd;
    private WaitForSeconds time_Delay = new WaitForSeconds(0.01f);
    private CharacterController character;
    [SerializeField] private GameObject hole_Flash;
    [SerializeField] private GameObject blood_Flash;
    [SerializeField] private GameObject boom_Flash;
    [HideInInspector] public bool isFiring;
    private bool isReloading;
    private float reloadTimer = 1f;
    private int bulletsUsed = 1;
    [SerializeField] private GameObject muzzle_flash_Parent;

    void Awake()
    {    
        weapon_Manager = GetComponent<WeaponManager>();
        character = GetComponent<CharacterController>();
        mainCam = Camera.main;

    }
    void Update()
    {   
        if(GetComponent<WeaponManager>().GetCurrentWeapon().currentBullets <= 0){
             return;
        }
        if(isReloading){
             return;
        }
        if(GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo <=0 ){
             StartCoroutine(Reload());
             return;
        }
        WeaponShoot();
    }
     private void WeaponShoot(){
        
        if(weapon_Manager.GetCurrentWeapon().fire_Type == FireType.SINGLE){
            if(isFiring){
                BulletFire();
                isFiring = false;
            }else{
                 GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//Revolver
        if(weapon_Manager.GetCurrentWeapon().bullet_Type == BulletType.LASER){
            if(isFiring){
                 LaserFire();
            }else{
                weapon_Manager.GetCurrentWeapon().WeaponSoundStop();
                GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//GunLight
        else if(weapon_Manager.GetCurrentWeapon().fire_Type == FireType.MULTIPLE){
            if(isFiring){
                BulletsFire();
            }else{
                weapon_Manager.GetCurrentWeapon().WeaponSoundStop();
                GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//ShortGun,MachineGun,AssaultRifle

    }
    private void BulletFire(){
             
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--; 
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed);
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation();

             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;
             //To move player frount and back
             StartCoroutine(GunMovementEffect());

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                 if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     HoleFlash(hit);
                     BoomMuzzleFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 12){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 7){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }
             }
    }
    private void BulletsFire(){


        if(Time.time >= nextFireRate){
             
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--;
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed); 
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation();

             nextFireRate = Time.time + 1/fireRate;
             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //Muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;
             //To move player frount and back
             StartCoroutine(GunMovementEffect());

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                  if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     BoomMuzzleFlash(hit);
                     HoleFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 12){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 7){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }
             }
        }
    }
    private void LaserFire(){

        

        if(Time.time >= nextFireRate){
              
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--;
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed);
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation(); 
             nextFireRate = Time.time + 1/fireRate;
             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //Muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;
             //To move player frount and back
             StartCoroutine(GunMovementEffect());

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                  if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     BoomMuzzleFlash(hit);
                     HoleFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 12){
                        //HoleMuzzleflash
                        HoleFlash(hit);                   
                        hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     hit.transform.GetComponent<HealthScript>().Damage(damaging);
                 }else if(hit.transform.gameObject.layer == 7){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }
             }
        }
    }
    private void BackForce(){
        character.Move(-transform.forward*0.075f);
    }
    private void FrountForce(){
        character.Move(transform.forward*0.05f);
    }
    private IEnumerator GunMovementEffect(){
        BackForce();
        yield return time_Delay;
        FrountForce();
    }
    private void HoleFlash(RaycastHit hit){
        GameObject hole = Instantiate(hole_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        hole.transform.parent = muzzle_flash_Parent.transform;
        Destroy(hole,0.25f);
    }
    private void BloodFlash(RaycastHit hit){
        GameObject blood = Instantiate(blood_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        blood.transform.parent = muzzle_flash_Parent.transform;
        Destroy(blood,0.25f);
    }
    private void BoomMuzzleFlash(RaycastHit hit){
        GameObject boom = Instantiate(boom_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        boom.transform.parent = muzzle_flash_Parent.transform;
        Destroy(boom,0.25f);
    }
    public void ButtonBulletStartFiring(){
               isFiring = true; 
    }
    public void ButtonBulletStopFiring(){
               isFiring = false;
    }
     
    private IEnumerator Reload(){
        isReloading = true;
        GetComponent<WeaponManager>().GetCurrentWeapon().StartReloadAnimation();
        GetComponent<WeaponManager>().GetCurrentWeapon().ReloadSoundPlay();
        yield return new WaitForSeconds(reloadTimer - 0.25f);
        GetComponent<WeaponManager>().GetCurrentWeapon().WeaponSoundStop();   
        GetComponent<WeaponManager>().GetCurrentWeapon().StopReloadAnimation();
        yield return new WaitForSeconds(0.25f);
        GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo = GetComponent<WeaponManager>().GetCurrentWeapon().maxAmmo;
        isReloading = false;
    }
}
