//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MedicalKitPickUp : MonoBehaviour
{
   private void Update()
    {
       WeaponRotation();
    }
    private void WeaponRotation(){
        Vector3 rotate = new Vector3(0,0,90);
        transform.Rotate((rotate)*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")){  
            gameObject.SetActive(false);        
            GameObject.FindGameObjectWithTag("PickUp").GetComponent<PickUpsManager>().WeaponCollectSound(true);
        }
        if( other.gameObject.GetComponent<HealthScript>().health != other.gameObject.GetComponent<HealthScript>().initialHealth){
            other.gameObject.GetComponent<HealthScript>().health = other.gameObject.GetComponent<HealthScript>().initialHealth;
        }
    }
}
