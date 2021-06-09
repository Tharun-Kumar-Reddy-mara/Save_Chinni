using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
  [SerializeField] private Image healtStats,staminaStats;
  [SerializeField] private GameObject blood_Scattering_Image_Of_The_Player;
  private float actualHealth;
  [SerializeField] private TextMeshProUGUI robotCount;

  private void Start()
  {
    blood_Scattering_Image_Of_The_Player.SetActive(false);
    actualHealth = GetComponent<HealthScript>().initialHealth;
  }
  private void Update()
  {
    robotCount.text = (GetComponent<RobotManager>().robotKilledCount).ToString();
  }
  public void DisplayHealthStats(float health){
      health /= actualHealth;
      healtStats.fillAmount = health;
  }
  public void DisplayStaminaStats(float stamina){
      stamina /= 100;
      staminaStats.fillAmount = stamina;
  }
  public void DisplyBloodStats(){
      StartCoroutine(BloodImage());
  }
  private IEnumerator BloodImage(){
     blood_Scattering_Image_Of_The_Player.SetActive(true);
     yield return new WaitForSeconds(0.1f);
     blood_Scattering_Image_Of_The_Player.SetActive(false);
  }
}
