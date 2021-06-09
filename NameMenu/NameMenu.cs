//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NameMenu : MonoBehaviour
{
   public TMP_InputField player_Name;
   private void Awake()
   {
       player_Name.text = PlayerPrefs.GetString("Name");
   }

   public void SaveNameButton(){
       PlayerPrefs.SetString("Name",player_Name.text);
       SceneManager.LoadScene("Main");
   }
   public void ResetNameButton(){
       PlayerPrefs.DeleteKey("Name");
   }
   public void Exit(){
       SceneManager.LoadScene("Main");
   }
  
}
