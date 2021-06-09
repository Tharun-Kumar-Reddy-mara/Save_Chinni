using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioSource level_Sound;
    [SerializeField] private Image [] lockImages;
    [SerializeField] private Image [] starImages;
    [SerializeField] private GameObject [] completedText;

    [SerializeField] private TextMeshProUGUI you_Need_To_Complete_Previous_One_To_Uncloc_This;

    private void Awake()
    {
        
        if(PlayerPrefs.GetFloat("LevelsName") < 1){
           PlayerPrefs.SetFloat("LevelsName",1);
        }

       // lock Images enable and disable
       for(int i = 0;i < lockImages.Length;i++){
            if(i <  PlayerPrefs.GetFloat("LevelsName")){
                lockImages[i].enabled = false;
            }
       }
       // completed text display
       for(int i = 0;i < completedText.Length;i++){
            if(i <  PlayerPrefs.GetFloat("LevelsName") - 1){
                completedText[i].SetActive(true);
                starImages[i].enabled = true;
                if(i < 1){
                   starImages[i].fillAmount = PlayerPrefs.GetFloat("Star"+1);
                }
                else{
                    int k = i + 1;
                    starImages[i].fillAmount = PlayerPrefs.GetFloat("Star"+k);
                } 
            }else{
                completedText[i].SetActive(false);
                starImages[i].enabled = false;
            }
       }
    }
    public void ExitButton(){
        SceneManager.LoadScene("Main");
    }

    public static void LevelConverter(float levelIndex ){
        
        DataTransfer.lillySaveTiming = 500/Mathf.Sqrt(levelIndex);
        DataTransfer.waitBeforSpawnRobots = 250/Mathf.Sqrt(levelIndex);
        DataTransfer.robotCount = levelIndex*levelIndex;
        DataTransfer.levelName = levelIndex;
        DataTransfer.robotDamage = 10 + (int)levelIndex;
        DataTransfer.robotHealth = 500 + levelIndex*levelIndex*levelIndex;

    }
    public static float LevelsStarIndicater(){
        if(DataTransfer.robotKilledCount >= DataTransfer.robotCount){
            return 1f;  
        }else if(DataTransfer.robotKilledCount >= (DataTransfer.robotCount)/2){
            return 0.7f;  
        }else{
            return 0.3f; 
        }
    }
    private void LevelsData(int val){

        //chages the data of all levels
        LevelConverter(val);

        //Audio
        if(lockImages[val - 1].enabled == false){

            level_Sound.Play();

            //Storeing levels name

            if(val > PlayerPrefs.GetFloat("LevelsName") ){
                PlayerPrefs.SetFloat("LevelsName",val);
            }

            //Assessing the Robot scene with some delay
            StartCoroutine(LoadingScreen()); 

        }else{
            StartCoroutine(UnableToAccessTheText());
        }

    }
    private IEnumerator UnableToAccessTheText(){
        you_Need_To_Complete_Previous_One_To_Uncloc_This.text = "You Need To Complete The Previous Levels To Unlock This".ToString();
        yield return new WaitForSeconds(2f);
        you_Need_To_Complete_Previous_One_To_Uncloc_This.text = " ".ToString();
    }
    public void Level1(int val){
        LevelsData(val);
    }public void Level2(int val){
        LevelsData(val);
    }public void Level3(int val){
        LevelsData(val);
    }public void Level4(int val){
        LevelsData(val);
    }public void Level5(int val){
        LevelsData(val);
    }public void Level6(int val){
        LevelsData(val);
    }public void Level7(int val){
        LevelsData(val);
    }public void Level8(int val){
        LevelsData(val);
    }public void Level9(int val){
        LevelsData(val);
    }public void Level10(int val){
        LevelsData(val);
    }
    private IEnumerator LoadingScreen(){
        yield return new WaitForSeconds(0.25f);
        GetComponent<LevelsLoading>().LoadingScreen();
    }

}
