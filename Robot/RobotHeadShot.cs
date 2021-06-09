
using UnityEngine;

public class RobotHeadShot : MonoBehaviour
{
    private int damage;
    private bool isFiring;
    private void Start()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>().GetCurrentWeapon().damage;
    }
    private void Update()
    {
        if(GetComponentInParent<HealthScript>().health <= 0){
              isFiring = false;
        }
        else{
              isFiring = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().isFiring;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(isFiring){
            GetComponentInParent<HealthScript>().Damage(damage + damage);
            GetComponentInParent<RobotStats>().DisplayHeadShot();
        }
    }
    
}
