//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RulesScript : MonoBehaviour
{
    [SerializeField] private GameObject rulesBG;
    private void Awake()
    {
        rulesBG.SetActive(false);
    }
    public void Rules(){
        rulesBG.SetActive(true);
    }
    public void Exit(){
        rulesBG.SetActive(false);
    }
}
