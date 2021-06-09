using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LillyTiming : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI game_Timer;
    public float actual_Timer = 500f;
    [HideInInspector] public float timer = 0f;
    private GameObject indicator;
    private void Awake()
    {
        actual_Timer = DataTransfer.lillySaveTiming;
        game_Timer.text = ((int)actual_Timer).ToString();
        indicator = GameObject.FindGameObjectWithTag("RedIndicator");
        indicator.SetActive(false);
    }
    void Update()
    {
       GameTimer();   
    }
    private void GameTimer(){

        if(actual_Timer <= 0f){
            return;
        }
        if(actual_Timer <= 20f){
            if(!indicator.activeInHierarchy){
               StartCoroutine(RedIndicator());
            }
        }

        timer += Time.deltaTime;
        if(timer >= 1f){
             actual_Timer -= 1f;
             timer = 0f;
        }
        game_Timer.text = ((int)actual_Timer).ToString();
    }
    private IEnumerator RedIndicator(){
        indicator.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        indicator.SetActive(false);
    }
}
