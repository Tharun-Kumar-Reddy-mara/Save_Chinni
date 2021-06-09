//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{   
    [SerializeField] private WeaponHandler [] weapons;
    [SerializeField] private List<WeaponHandler> guns;
    private GameObject crossHairImage;
    private int currentWeaponIndex;
    private int finalWeaponIndex = 0;
    public int FinalWeaponIndex{get {return finalWeaponIndex;}}

    //List data 
    
    void Start()
    {
        currentWeaponIndex = 0;
        guns = new List<WeaponHandler>();
        UnlockWeapon();
        guns[currentWeaponIndex].gameObject.SetActive(true);
        crossHairImage = GameObject.FindGameObjectWithTag("CrossHair");
    }
    private void UnlockWeapon()
    {
       if(PlayerPrefs.GetInt("WeaponIndex") > 0){ 

           GunsPickupIncrement(PlayerPrefs.GetInt("WeaponIndex"));

       }else{
           GunsPickupIncrement(0);
       }
    }
    private void TurnOnSelectedWeapon(int weaponIndex){
        if(currentWeaponIndex == weaponIndex){
            return;
        }
        guns[currentWeaponIndex].gameObject.SetActive(false);
        guns[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;        
    }
    public WeaponHandler GetCurrentWeapon(){

        return guns[currentWeaponIndex];

    }
    public void GunChanging(){
        finalWeaponIndex ++;

        if(finalWeaponIndex >= guns.Count){
            finalWeaponIndex = 0;
        }

        TurnOnSelectedWeapon(finalWeaponIndex);
       
    }
    public void GunsPickupIncrement(int index){

    
            if(!guns.Contains(weapons[index])){
               guns.Add(weapons[index]);
            }else{
               guns[currentWeaponIndex].GetComponent<WeaponHandler>().currentBullets = guns[currentWeaponIndex].GetComponent<WeaponHandler>().maxBullets;
            }

    }
     private void CrossHairEnabling(){
        if(GetCurrentWeapon().bullet_Type == BulletType.LASER){
              crossHairImage.SetActive(false);
        }else{
              crossHairImage.SetActive(true);
        } 
    }

}
