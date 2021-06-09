//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    private Transform target;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bullet_Speed = 40f;
    [SerializeField] private float attack_Timer;
    [SerializeField] private float wait_Before_Attack = 0.25f;
    
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        if(GameObject.FindGameObjectWithTag("Lilly").GetComponent<LillyTiming>().actual_Timer <= 20f){
              target = GameObject.FindWithTag("Lilly").transform;
        }
    }
    public void Fire(){
         attack_Timer += Time.deltaTime;
        if(attack_Timer > wait_Before_Attack){
            GameObject bullets = Instantiate(bullet,transform.position,Quaternion.identity);
            Rigidbody rb = bullets.GetComponent<Rigidbody>();
            rb.velocity = (target.position - transform.position).normalized * bullet_Speed + new Vector3(0,3f,0); 
            Destroy(bullets, 0.5f);
            attack_Timer = 0f;
        }
    }
    private void OnEnable()
    {
        Fire();
    }

}
