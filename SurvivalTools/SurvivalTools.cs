//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SurvivalTools : MonoBehaviour
{
    private float radius = 2.5f;
    [SerializeField] private LayerMask layerMask;
    private void Awake()
    {
        transform.Rotate(90,0,0);
    }
    void Update()
    {
        ToolsRotation();
        HealthIncrement();
    }
    private void ToolsRotation(){
         Vector3 rotate = new Vector3(0,0,90);
         transform.Rotate((rotate)*Time.deltaTime);
    }
    private void HealthIncrement(){
        Collider [] hits = Physics.OverlapSphere(transform.position,radius,layerMask);
        if(hits.Length > 0){
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().blood_Incresed = true;
            GameObject.FindGameObjectWithTag("PickUp").GetComponent<PickUpsManager>().WeaponCollectSound(true);
            gameObject.SetActive(false);
        }
    }
    
}
