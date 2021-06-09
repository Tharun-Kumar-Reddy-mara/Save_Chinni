//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponStats : MonoBehaviour
{
   [SerializeField] private Sprite[] gunImages;
   [SerializeField] private List<Sprite> sprites;
   [SerializeField] private Image imageContainer;
   private int initialGunIndex = 0;
   private int totalBullets;
   private int currentBullets;
   private int unlockIndex;
   [SerializeField] private WeaponManager weaponManager;
   [SerializeField] private TextMeshProUGUI bulletsUi;
   
   private void Awake()
   {    
        sprites = new List<Sprite>();
        UnlockImage();
        sprites.Add(gunImages[unlockIndex]);
        imageContainer.sprite = sprites[initialGunIndex];
   }
   private void UnlockImage(){
     if(PlayerPrefs.GetInt("WeaponIndex") > 0){   
          unlockIndex = PlayerPrefs.GetInt("WeaponIndex");
     }else{
         unlockIndex = 0;
     }   
   }
   
   private void Update()
   {
       BulletsUi();
   }
   public void GunImageChanging(){
        initialGunIndex++;
        if(initialGunIndex >= sprites.Count){
              initialGunIndex = 0;
        }
        imageContainer.sprite = sprites[initialGunIndex];
   }
   public void GunPickupImageIncrement(int index){
        if(!sprites.Contains(gunImages[index])){
               sprites.Add(gunImages[index]);
            }

   }
   private void BulletsUi(){
        bulletsUi.text = (weaponManager.GetCurrentWeapon().currentBullets + "/" + weaponManager.GetCurrentWeapon().maxBullets).ToString();
   }

}
