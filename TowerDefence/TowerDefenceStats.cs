//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenceStats : MonoBehaviour
{
    [SerializeField] private Image blood_Stats;
   private float actualHealth;

   private void Start()
   {
       actualHealth = GetComponent<HealthScript>().initialHealth;
   }
   public void DisplayBloodStats(float health){
       health /= actualHealth;
       blood_Stats.fillAmount = health;
   }
}
