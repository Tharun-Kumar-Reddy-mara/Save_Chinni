using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LillyStats : MonoBehaviour
{   
    [SerializeField] private Image healtStats;

    private float actualHealth;
    [SerializeField] private TextMeshProUGUI lilly_Distance;
    private Transform player;
    private float distance_Of_Lilly;
    private void Start(){
      actualHealth = GetComponent<HealthScript>().initialHealth;
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
       distance_Of_Lilly = (transform.position - player.position).magnitude;
       lilly_Distance.text = ((int)distance_Of_Lilly).ToString()+"M";
    }
    public void DisplayHealthStats(float health){
      health /= actualHealth;
      healtStats.fillAmount = health;
    }

}
