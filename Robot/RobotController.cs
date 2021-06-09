using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum StateOfRobot{
    PATROL,
    CHASE,
    ATTACK,
}

public class RobotController : MonoBehaviour
{ 
    private RobotAnimator robot_Animator;
    private NavMeshAgent navAgent;
    private float turnRate = 5f;
    private Transform target;
    private Transform hostage;
    private Transform finalTarget;
    [SerializeField] private float patrol_Radius_Min = 20f,patrol_Radius_Max = 60f;
    [SerializeField] private float patrol_For_This_Time = 10f;
    private float patrol_Timer;
    [SerializeField] private float walk_Speed = 3f;
    [SerializeField] private float run_Speed = 6f;
    public float chase_Distance = 30f;
    [SerializeField] private float current_Chase_Distance;
    [SerializeField] private float attack_Timer;
    [SerializeField] private float wait_Before_Attack = 0.25f;
    [SerializeField] private float attack_Distance = 2f;
    [SerializeField] private float chase_After_Attack = 2.5f;
    private StateOfRobot state_Of_Robot;
    public RobotSounds robot_Sounds;
    [SerializeField] private GameObject gunEnd;
    [SerializeField] private GameObject muzzleFlash;


    void Awake()
    {
        robot_Animator = GetComponent<RobotAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        hostage = GameObject.FindGameObjectWithTag("Lilly").transform;
        finalTarget = GameObject.FindGameObjectWithTag("FinalTarget").transform;
    }
    void Start()
    {
        state_Of_Robot = StateOfRobot.PATROL;
        current_Chase_Distance = chase_Distance;
        attack_Timer = wait_Before_Attack;
        patrol_Timer = patrol_For_This_Time;
    }
    void Update()
    {
        
        if(state_Of_Robot == StateOfRobot.PATROL){
             Patrol();
        }
        if(state_Of_Robot == StateOfRobot.CHASE){
             Chase();
        }
        if(state_Of_Robot == StateOfRobot.ATTACK){
             Attack();
             if((transform.position - target.position).magnitude >= 5f){
                  LookAroundPlayer();
             }
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().health <= 0f){
              target = finalTarget;
              state_Of_Robot = StateOfRobot.PATROL;
              chase_Distance = 0.1f;
              
        }
        if(GameObject.FindGameObjectWithTag("Lilly").GetComponent<LillyTiming>().actual_Timer <= 20f){

            if(target != hostage){
                 target = hostage;
            }
            chase_Distance = 100f;
        }
        if(GameObject.FindGameObjectWithTag("Lilly").GetComponent<HealthScript>().health <= 0f){
              hostage = finalTarget;
              state_Of_Robot = StateOfRobot.PATROL;
              chase_Distance = 0.1f;
        }
    }
    private void Patrol(){
          navAgent.isStopped = false;
          navAgent.speed = walk_Speed;
          patrol_Timer += Time.deltaTime;
          if(patrol_Timer > patrol_For_This_Time){
            SetNewRandomDestination();
            patrol_Timer = 0f;
          }
          if(navAgent.velocity.sqrMagnitude > 0){
              robot_Animator.Walk(true);
          }else{
              robot_Animator.Walk(false);
          }
          if(Vector3.Distance(transform.position,target.position) <= chase_Distance ){
              robot_Sounds.ScreamSound();
              robot_Animator.Walk(false);
              state_Of_Robot = StateOfRobot.CHASE;
          }
    }
    private void Chase(){
          navAgent.isStopped = false;
          navAgent.speed = run_Speed;
          navAgent.SetDestination(target.position);
          chase_Distance = 100f;
          if(navAgent.velocity.sqrMagnitude > 0){
              robot_Animator.Run(true);
          }else{
              robot_Animator.Run(false);
          }
          if(Vector3.Distance(transform.position,target.position) <= attack_Distance ){
              robot_Animator.Run(false);
              robot_Animator.Walk(false);
              state_Of_Robot = StateOfRobot.ATTACK;
          }else if(Vector3.Distance(transform.position,target.position) > chase_Distance){
              robot_Animator.Run(false);
              state_Of_Robot = StateOfRobot.PATROL;
          }
    }
    private void Attack(){
        navAgent.isStopped = true; 
        navAgent.velocity = Vector3.zero;
        attack_Timer += Time.deltaTime; 
        if(attack_Timer > wait_Before_Attack){ 
           StartCoroutine(MuzzleFlash());
           robot_Sounds.AttackSound(); 
           robot_Animator.Attack(true); 
           attack_Timer = 0f;
        }
        else{
           robot_Animator.Attack(false);
        }
        if(Vector3.Distance(transform.position,target.position) > attack_Distance + chase_After_Attack ){
              robot_Animator.Attack(false);
              state_Of_Robot = StateOfRobot.CHASE;
        }

    }
    private void LookAroundPlayer(){
        Vector3 targetDelta = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward,targetDelta);
        Vector3 turnAxis = Vector3.Cross(transform.forward,targetDelta);
        transform.RotateAround(transform.position,turnAxis,Time.deltaTime*angleToTarget*turnRate);
    }
    
    public void StartFiring(){
        gunEnd.SetActive(true);
    }
    public void StopFiring(){
        gunEnd.SetActive(false);
    }
    private IEnumerator MuzzleFlash(){
        yield return new WaitForSeconds(0.01f);
        GameObject flash = Instantiate(muzzleFlash,gunEnd.transform.position,Quaternion.identity);
        Destroy(flash,0.05f);
    }
    private void SetNewRandomDestination(){
         float rand_Radius = Random.Range(patrol_Radius_Min,patrol_Radius_Max);
         Vector3 randDir = Random.insideUnitSphere*rand_Radius;
         randDir += transform.position;

         NavMeshHit navHit;
         
         NavMesh.SamplePosition(randDir,out navHit,rand_Radius,-1);
         navAgent.SetDestination(navHit.position);
         
    }
    public StateOfRobot RobotState{
          get;
          set;
    }

}
