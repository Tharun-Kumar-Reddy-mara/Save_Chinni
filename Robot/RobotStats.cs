using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStats : MonoBehaviour
{
   [SerializeField] private Image blood_Stats;
   private float actualHealth;
   private GameObject head_Shot;

   private void Start()
   {
       actualHealth = GetComponent<HealthScript>().initialHealth;
   }
   private void Awake()
   {
       head_Shot = GameObject.FindGameObjectWithTag("HeadShot");
       head_Shot.SetActive(false);
   }
   public void DisplayBloodStats(float health){
       health /= actualHealth;
       blood_Stats.fillAmount = health;
   }
   public void DisplayHeadShot(){
        if(!head_Shot.activeInHierarchy){
           StartCoroutine(HeadShot());
        }
   }
   private IEnumerator HeadShot(){
       head_Shot.SetActive(true);
       yield return new WaitForSeconds(0.25f);
       head_Shot.SetActive(false);
   }
}