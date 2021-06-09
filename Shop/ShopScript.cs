using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopScript : MonoBehaviour
{ 
    [SerializeField] private Sprite[] gunImages;
    [SerializeField] private Image[] lockImages;
    [SerializeField] private Image image_Holder;
    [SerializeField] private TextMeshProUGUI weapon_Name;
    [SerializeField] private TextMeshProUGUI bullets;
    [SerializeField] private TextMeshProUGUI reload_Speed;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private WeaponHandler[] weaponHandler;
    [SerializeField] private GameObject shop_Menu;
    [SerializeField] private TextMeshProUGUI robot_Killed_Count;
    private int index_To_Change_Gun;
    private bool enough_Kills = false;
    private bool selected_Weapon = false;
    [SerializeField] private AudioSource buy_Audio;
    [SerializeField] private TextMeshProUGUI not_Enough_Kills_Text;

    private void Awake()
    {
        lockImages[0].enabled = false;
        robot_Killed_Count.text = ((int)PlayerPrefs.GetFloat("RobotsKilled")).ToString();
  
    }
    private void OnEnable()
    {
        if(PlayerPrefs.GetFloat("RobotsKilled") <= 0){
             lockImages[0].enabled = false;
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 10){
             lockImages[1].enabled = false;     
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 25){
             lockImages[2].enabled = false;     
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 50){
             lockImages[3].enabled = false;     
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 75){
             lockImages[4].enabled = false;     
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 100){
             lockImages[5].enabled = false;     
        }else if(PlayerPrefs.GetFloat("RobotsKilled") > 150){
             lockImages[6].enabled = false;     
        }
    }
    private void gunDetails(int index){
        image_Holder.sprite = gunImages[index];
        weapon_Name.text = weaponHandler[index].name.ToString();
        bullets.text = "Bullets" + " : " + weaponHandler[index].maxBullets.ToString();
        reload_Speed.text = "Reload" + " : " + weaponHandler[index].maxAmmo.ToString();
        damage.text = "Damage" + " : " + weaponHandler[index].damage.ToString();
        range.text = "Range" + " : " + ((int)weaponHandler[index].weapon_Range).ToString();
        index_To_Change_Gun = index;
    }
    public void MashVirtual(){
        gunDetails(0);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 0){
             enough_Kills = true;
        }
        else{
             enough_Kills = false;
        }
        
    }
    public void GrimBrand(){
        gunDetails(1);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 10){
            enough_Kills = true;
        }
        else{
             enough_Kills = false;
        }
    }
    public void Mauler(){
        gunDetails(2);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 25){
             enough_Kills = true;
        }
        else{
             enough_Kills = false;
        }
    }
    public void Marker(){
        gunDetails(3);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 50){
            enough_Kills = true;
        }
        else{
             enough_Kills = false;
        }
    }
    public void Archtronic(){
        gunDetails(4);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 75){
             enough_Kills = true;
        }
        else{
            enough_Kills = false;
        }
    }
    public void HellWailer(){
        gunDetails(5);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 100){
             enough_Kills = true;
        }
        else{
            enough_Kills = false;
        }
    }
    public void FireSleet(){
        gunDetails(6);
        selected_Weapon = true;
        if(PlayerPrefs.GetFloat("RobotsKilled") >= 150){
            enough_Kills = true;
        }
        else{
             enough_Kills = false;
        }
    }
    public void Exit(){
        shop_Menu.SetActive(false);
    }
    public void OnPressBuy(){
        if(enough_Kills){
            PlayerPrefs.SetInt("WeaponIndex",index_To_Change_Gun);
            buy_Audio.Play();
            enough_Kills = false;
        }else if(!selected_Weapon){
            not_Enough_Kills_Text.text = "Select The Gun".ToString();
        }
        else{
            not_Enough_Kills_Text.text = "No Enough Kills".ToString();
        }
    }
    public void OnUpBuy(){
        StartCoroutine(NotEnoughKillsText());
    }
    private IEnumerator NotEnoughKillsText(){

        yield return new WaitForSeconds(1.5f);
        not_Enough_Kills_Text.text = "".ToString();

    }
   

}
