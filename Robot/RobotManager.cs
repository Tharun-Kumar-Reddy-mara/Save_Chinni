using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RobotManager : MonoBehaviour
{
    public static RobotManager instance;
    [HideInInspector] public float robotKilledCount;
    [SerializeField] private GameObject [] robots;
    [SerializeField] private GameObject  tower_Defence;
    [SerializeField] private Transform [] robots_Spawn_Points;
    [SerializeField] private Transform [] tower_Defence_Spawn_Points;
    [SerializeField] private float robots_Count;
    private float initial_Robots_Killed_Count = 0;
    [SerializeField] private float tower_Defence_Count;
    private float initial_Robots_Count;
    private float initial_Tower_Defence_Count;
    [SerializeField] private float wait_Befor_Spawn_Robots = 60f;
    [SerializeField] private TextMeshProUGUI level_Name;
    private void Awake()
    {
        MakeInstance();
        level_Name.text = "Level" + " " + ((int)DataTransfer.levelName).ToString();
        wait_Befor_Spawn_Robots = DataTransfer.waitBeforSpawnRobots;
        robots_Count = DataTransfer.robotCount;
    }
    private void OnDisable() {
        PlayerPrefs.SetFloat("RobotsKilled",PlayerPrefs.GetFloat("RobotsKilled") + robotKilledCount);
        DataTransfer.robotKilledCount = robotKilledCount;
    }

    private void MakeInstance(){
        if(instance == null){
              instance = this;
        }
    }
    private void Start() {
        robotKilledCount = initial_Robots_Killed_Count;
        initial_Robots_Count = robots_Count;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    private void SpawnEnemies(){
        SpawnRobots();
        SpawnTowerDefence();
    }
    private void SpawnRobots(){

        int spawn_Index = 0;
        int robot_Index = 0;
        for(int i = 0;i < robots_Count;i++){
            if(spawn_Index >= robots_Spawn_Points.Length){
                spawn_Index = 0;
            }
            if(robot_Index >= robots.Length){
                robot_Index = 0;
            }
            Instantiate(robots[robot_Index],robots_Spawn_Points[spawn_Index].position,Quaternion.identity);
            spawn_Index++;
            robot_Index++;
        }

        robots_Count = 0;
        
    }//Spawn Robots
    private void SpawnTowerDefence(){

        int spawn_Index = 0;
        for(int i = 0;i < tower_Defence_Count;i++){
            if(spawn_Index >= tower_Defence_Spawn_Points.Length){
                spawn_Index = 0;
            }
            Instantiate(tower_Defence,tower_Defence_Spawn_Points[spawn_Index].position,Quaternion.identity);
            spawn_Index++;

        }

        tower_Defence_Count = 0;
        
    }//Spawn TowerDefence
   
    public void RobotDied(bool robots){
        if(robots){
             robotKilledCount++;
             robots_Count++;
             if(robots_Count > initial_Robots_Count){
                robots_Count = initial_Robots_Count;
             }
        }else{
            robotKilledCount++;
            tower_Defence_Count++;
            if(tower_Defence_Count > initial_Tower_Defence_Count){
                tower_Defence_Count = initial_Tower_Defence_Count;
             }
        }
    }//reproducing the death robots && TowerDefence

    private IEnumerator CheckToSpawnEnemies(){
        yield return new WaitForSeconds(wait_Befor_Spawn_Robots);
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    public void StopSpawning(){
        StopCoroutine("CheckToSpawnEnemies");
    }//Stop Spawning

}
