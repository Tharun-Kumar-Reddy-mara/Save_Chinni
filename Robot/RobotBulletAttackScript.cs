//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RobotBulletAttackScript : MonoBehaviour
{    [SerializeField] private int damage = 10;
     private void Awake()
     {
        damage = DataTransfer.robotDamage;
     }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
                other.gameObject.GetComponent<HealthScript>().Damage(damage);
        }else if(other.gameObject.CompareTag("Lilly")){
                other.gameObject.GetComponent<HealthScript>().Damage(damage);
        }
    }
}
