using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lilly : MonoBehaviour
{
  [SerializeField] private LayerMask layer_Of_Savior;
  private float radius = 5f;
  [HideInInspector] public bool winSound = false;
  private Collider [] hit;
  private void Update()
  {
      hit = Physics.OverlapSphere(transform.position,radius,layer_Of_Savior); 
      if(hit.Length > 0){
          if(GetComponent<HealthScript>().health > 0){
               GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAudioScript>().WinSound();
               StartCoroutine(LoadScene());  
          }      
      }
  }
  private IEnumerator LoadScene(){
      yield return new WaitForSeconds(0.25f);
      SceneManager.LoadScene("Win");
  }
}
