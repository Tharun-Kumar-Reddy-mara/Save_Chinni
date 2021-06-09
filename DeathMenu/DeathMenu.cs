//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loser_Text;  
    private GameObject backgroundImg;
    public Image deathMenu;
    [SerializeField] private AudioSource deathSound;
    private bool isShowed = false;
    private float transition = 0.0f;
    private void Awake()
    {
        backgroundImg = GameObject.FindGameObjectWithTag("DeathUi");
        backgroundImg.SetActive(false);
    }
    private void OnEnable()
    {
        loser_Text.text = " '' "+ PlayerPrefs.GetString("Name").ToString() + " '' " + "You Lost !!!";
    }
    private void Update()
    {
        if(isShowed){
            transition += Time.deltaTime;
            deathMenu.color = Color.Lerp(new Color(0,0,0,0),Color.black,transition);
            return;
        }
    }
    
    public void ToggleEndMenu(){
        
        backgroundImg.SetActive(true);
        deathSound.Play();
        isShowed = true;
    }
    public void Menu(){
        SceneManager.LoadScene("Main");
    }
    public void Restart(){

        LevelManager.LevelConverter(DataTransfer.levelName);
        GetComponent<DeathLoading>().LoadingScreen();

    }
    public void Exit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();   
        #endif
    }
}
