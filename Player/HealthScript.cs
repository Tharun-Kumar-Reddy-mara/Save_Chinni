using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [HideInInspector] public float initialHealth; 
    public float health = 500;
    public bool is_Robot,is_Player,is_Lilly,is_Tower_Defence;
    private bool is_Dead;
    private RobotAnimator robot_Anim;
    private RobotController robot_Controller;
    private PlayerMovement player_Movement;
    private NavMeshAgent nav_Mesh;
    private RobotSounds robot_Sounds;
    private PlayerStats playerStats;
    private RobotStats robotStats;
    private Animator lilly_Anim;
    private LillySounds lilly_Sounds;
    private LillyStats lilly_Stats;
    private TowerDefenceSound towerDefenceSound;
    private TowerDefenceStats tower_Stats;
    [HideInInspector] public bool blood_Incresed = false;
    
    void Awake(){

        if(is_Robot){
            robot_Anim = GetComponent<RobotAnimator>();
            robot_Controller = GetComponent<RobotController>();
            nav_Mesh = GetComponent<NavMeshAgent>();
            robot_Sounds = GetComponent<RobotSounds>();
            robotStats = GetComponent<RobotStats>();
            health = DataTransfer.robotHealth;
        }
        if(is_Player){
            playerStats = GetComponent<PlayerStats>();
        }
        if(is_Lilly){
            lilly_Anim = GetComponent<Animator>();
            lilly_Sounds = GetComponent<LillySounds>();
            lilly_Stats = GetComponent<LillyStats>();
        }
        if(is_Tower_Defence){
            tower_Stats = GetComponent<TowerDefenceStats>();
        }

        initialHealth = health;
    }
    private void Update()
    {   
        if(is_Player){
            
            if(blood_Incresed){

                if(health < initialHealth){
                     health = initialHealth;
                     playerStats.DisplayHealthStats(health);
                }
                else{
                     blood_Incresed = false;
                }
            }
           
        }
    }
    public void Damage(int damage){
           if(is_Dead){
               return;
           }
           health -= damage;
           if(health <= 0 ){
               Dead();
               is_Dead = true;
           }
           //To chase the player
           if(is_Robot){
               if(robot_Controller.RobotState == StateOfRobot.PATROL){
                    robot_Controller.chase_Distance = 100f;
               }
           }
           //Player Ui health
           if(is_Player){
                 playerStats.DisplayHealthStats(health);
                 playerStats.DisplyBloodStats();
                 GetComponentInChildren<PlayerAudioScript>().DamageSound();
           }
           //Robot ui
            if(is_Robot ){
                 robotStats.DisplayBloodStats(health);                
           }
           //LillY ui
           if(is_Lilly){
                 lilly_Stats.DisplayHealthStats(health);
                 GetComponent<LillySounds>().DamageSound();
           }
           //TowerDefence Ui
           if(is_Tower_Defence){
                 tower_Stats.DisplayBloodStats(health);
           }

    }
    private void Dead(){
        if(is_Robot){

            nav_Mesh.velocity = Vector3.zero;
            nav_Mesh.isStopped = true; 
            robot_Controller.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<RobotStats>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
            robot_Anim.Die();
            StartCoroutine(DeadSound());
            RobotManager.instance.RobotDied(true);
            GameObject.FindGameObjectWithTag("PickUp").GetComponent<PickUpsManager>().PickupSpawning(transform);
    
        }
        if(is_Player){
            //To inactive weapon & player
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerSprintAndCrouch>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponentInChildren<PlayerAudioScript>().DeathSound();
            GameObject.FindGameObjectWithTag("Ambience").SetActive(false);
            RobotManager.instance.StopSpawning();
            
        }
        if(is_Lilly){
            lilly_Anim.SetTrigger("Die");
            lilly_Sounds.DeathSound();
            RobotManager.instance.StopSpawning();
        }
        if(is_Tower_Defence){
            GetComponent<TowerDefenceSound>().DeadSound();
            GetComponent<GatlingGun>().enabled = false;
            GetComponent<TowerDefenceStats>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<LODGroup>().enabled = false;
            GetComponent<TowerDefenceStats>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
            RobotManager.instance.RobotDied(false);
        }
        if( (tag == "Player" || tag == "Lilly" ) || GameObject.FindGameObjectWithTag("Lilly").GetComponent<LillyTiming>().actual_Timer <= 0f ){
            Invoke("RestartGame",2.5f);
        }else if(tag == "TowerDefence"){
            Invoke("KillGameObject",0.5f);
        }else{
            Invoke("KillGameObject",4f);
        }
    }//Dead

    private void RestartGame(){
       GameObject.FindGameObjectWithTag("DeathMenu").GetComponent<DeathMenu>().ToggleEndMenu();
       if(is_Player){
           GetComponentInChildren<PlayerAudioScript>().enabled = false;
       }
       GameObject [] robots = GameObject.FindGameObjectsWithTag("Enemy");
       GameObject [] towers = GameObject.FindGameObjectsWithTag("TowerDefence");
       foreach(GameObject robot in robots){
            robot.SetActive(false);
       }
       foreach(GameObject tower in towers){
            tower.SetActive(false);
       }
       GameObject.FindGameObjectWithTag("Lilly").SetActive(false);
    }
    private void KillGameObject(){
        gameObject.SetActive(false);
    }
    private IEnumerator DeadSound(){ 
        yield return new WaitForSeconds(0.5f);
        robot_Sounds.DeathSound();
    }
   
}
