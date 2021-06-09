//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject shop_Menu;
   [SerializeField] private GameObject video_Menu;
   [SerializeField] private VideoPlayer video;
   private void Awake()
   {     
        shop_Menu.SetActive(false);

        // Demo Video
        if(PlayerPrefs.GetFloat("LevelsName") < 1 ){
            video_Menu.SetActive(true);
        }
        else{
            video.Stop();
            video.enabled = false;
            video_Menu.SetActive(false); 
        }
   }
   private void Update()
   {
        if(Time.time > 36f){
             video.Stop();
             video.enabled = false;
             video_Menu.SetActive(false);          
        }
   }
   
   public void ExitGame(){
       #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
       #else
            Application.Quit();   
       #endif       
   }
   public void Levels(){
       SceneManager.LoadScene("Levels");
   }
   public void Shop(){
         shop_Menu.SetActive(true);
   }
   public void Name(){
        SceneManager.LoadScene("Name");
   }
 
}
