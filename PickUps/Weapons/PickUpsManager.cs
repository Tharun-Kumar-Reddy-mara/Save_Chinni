//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PickUpsManager : MonoBehaviour
{
   [SerializeField] private GameObject [] gun_PickUps;
   [SerializeField] private GameObject medical_Kit;
   [SerializeField] private GameObject Pickup_MuzzleFlash;
   [SerializeField] private AudioClip weapon_Collect_Sound;
   [SerializeField] private AudioSource weapon_Audio_Source;
   private Vector3 offset_height;
   private int spawn_Count;
   public void PickupSpawning( Transform pos){
        spawn_Count++;
        //weapon offset for positioning
        if(gun_PickUps[2]){
              offset_height = new Vector3(0,1.75f,0); 
        }
        else{
             offset_height = new Vector3(0,2.5f,0);
        }
        //spawning items 
        if(spawn_Count == 3 ){
             SpawningWeapon(pos);
        }else if(spawn_Count == 5){
             SpawningMedicalKit(pos);
             spawn_Count = 0;
        }
        
   }
   public void WeaponCollectSound(bool collected){
        if(collected){
            weapon_Audio_Source.clip = weapon_Collect_Sound;
            weapon_Audio_Source.Play();
        }
   }
   private void SpawningWeapon(Transform pos){
        GameObject pickup = Instantiate(gun_PickUps[Random.Range(0,gun_PickUps.Length)],pos.position + offset_height ,Quaternion.identity);
        GameObject muzzleflash = Instantiate(Pickup_MuzzleFlash,pos.position,Quaternion.identity);
        muzzleflash.transform.localRotation = Quaternion.Euler(-90f,0,0);
        Destroy(muzzleflash,20f);
        Destroy(pickup,20f);
   }
   private void SpawningMedicalKit(Transform pos){
        GameObject medicalKit = Instantiate(medical_Kit,pos.position + offset_height ,Quaternion.identity);
        GameObject muzzleflash = Instantiate(Pickup_MuzzleFlash,pos.position,Quaternion.identity);
        muzzleflash.transform.localRotation = Quaternion.Euler(-90f,0f,0);
        Destroy(muzzleflash,20f);
        Destroy(medicalKit,20f);
   }
}
