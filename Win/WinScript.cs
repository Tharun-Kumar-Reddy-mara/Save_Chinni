//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI win_Text;
    [SerializeField] private AudioSource win_Audio;

    private void Awake()
    {
        win_Text.text = " '' " + PlayerPrefs.GetString("Name").ToString() + " '' " + "You Won !!!";
        if(PlayerPrefs.GetFloat("LevelsName") <= DataTransfer.levelName){
            PlayerPrefs.SetFloat("LevelsName" , PlayerPrefs.GetFloat("LevelsName") + 1);
        }
        string level_Star_Value = "Star" + (DataTransfer.levelName).ToString();
        PlayerPrefs.SetFloat(level_Star_Value,LevelManager.LevelsStarIndicater());
        Debug.Log(PlayerPrefs.GetFloat("Star1")); 
    }

    public void Exit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();   
        #endif
    }
    public void Menu(){
        SceneManager.LoadScene("Main");
    }
    public void Replay(){
        win_Audio.enabled = false;
        LevelManager.LevelConverter(DataTransfer.levelName);
        SceneManager.LoadScene("Robot");
        GetComponent<WinLoading>().LoadingScreen();
    }
    public void NextLevel(){
        win_Audio.enabled = false;
        LevelManager.LevelConverter(DataTransfer.levelName + 1);
        SceneManager.LoadScene("Robot");
        GetComponent<WinLoading>().LoadingScreen();
    }
}
