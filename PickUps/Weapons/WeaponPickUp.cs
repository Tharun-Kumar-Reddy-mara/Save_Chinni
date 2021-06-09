//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public int weapon_Index;
    private void Update()
    {
       WeaponRotation();
    }
    private void WeaponRotation(){
        Vector3 rotate = new Vector3(0,90,0);
        transform.Rotate((rotate)*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")){  
            gameObject.SetActive(false);        
            GameObject.FindGameObjectWithTag("PickUp").GetComponent<PickUpsManager>().WeaponCollectSound(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>().GunsPickupIncrement(weapon_Index);  
            GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponStats>().GunPickupImageIncrement(weapon_Index);      
        }
    }

}
