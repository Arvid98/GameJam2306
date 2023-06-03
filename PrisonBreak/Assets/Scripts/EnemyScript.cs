using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    
    private enum State
    {
        Patrol,
        ChasePlayer,
        AttackPlayer,
    }
    [Header("Current Behavior (Default = Patrol)")]
    [SerializeField] private State currentState;

    [Header("Target Object (Usually Player)")]
    [Tooltip("The target of the enemy. This will usually be the player object")]
    public Transform target;

    private float currentSpeed;
    private float walkSpeed;
    private float chaseSpeed;

    private NavMeshAgent agent;

    private float chaseDistance; 

    //Variable for where the enemy should look
    private Vector3 lookAt;

    //Variables for patrol points
    [Header("List of Patrol Points. (Fill in before starting)")]
    [SerializeField] private List<GameObject> PatrolPointList = new List<GameObject>();
    private int currentPointIndex;

    //Variables for FOV
    [Header("FOV Variables")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform fovPrefab;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    private FieldOfView fieldOfView;



    void Start()
    {
        //Initializing agent variables necessary for the agent to work in 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentPointIndex = 0;
        currentSpeed = 3.5f;
        chaseDistance = 20f;


        fov = 90f;
        viewDistance = 10f;

        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDistance(viewDistance);


    }

    // Update is called once per frame
    void Update()
    {
        //Stops all enemy behavior is the player is dead.
        /*
        if (PlayerMovement.IsDead) //Update to fit if necessary
        {
            return;
        }
        */

        //Set the agent speed to current speed;
        agent.speed = currentSpeed;

        //Rotate to look at target
        var direction = lookAt - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //State machine for the AI
        switch (currentState)
        {
            default:
                break;

            case State.Patrol:
                //All behavir for the AI to do while patrolling

                //Move to the current patrol point, and change to the next in the list when reached
                if(PatrolPointList.Count !=0)
                {
                    agent.destination = PatrolPointList[currentPointIndex].transform.position;
                    if (Vector2.Distance(transform.position, PatrolPointList[currentPointIndex].transform.position) < 0.5)
                    {
                        if (currentPointIndex >= PatrolPointList.Count - 1)
                        {
                            currentPointIndex = 0;
                        }
                        else
                        {
                            currentPointIndex++;
                        }

                        //Make the enemy look towards next point
                        lookAt = PatrolPointList[currentPointIndex].transform.position;
                    }


                }

                //Look at next patrol point
                if (PatrolPointList.Count != 0)
                {
                    lookAt = PatrolPointList[currentPointIndex].transform.position;
                }

                //Constantly checks if target is visible
                CheckForTarget();

                break;
            case State.ChasePlayer:
                //All behavior for the AI to do while chasing the player

                agent.destination = target.transform.position;
                lookAt = target.transform.position;

                //Code to make it switch to attack state if necessary

                CheckOutOfChaseRange();

                break;
            case State.AttackPlayer:
                //All behavior for the AI to do while attacking the player

                break; 
        }

        //Update fov mesh
        fieldOfView.SetAimDirection(direction);
        fieldOfView.SetOrigin(transform.position);
    }

    //Called when the enemy should check if the target is inside their vision
    private void CheckForTarget()
    {
        //Check if target is inside view distance of the enemy
        if(Vector2.Distance(transform.position, target.transform.position) <= viewDistance)
        {
            Debug.Log("INSIDE VIEW DISTANCE");

            //Check if target is inside angle of the fov
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            var direction = lookAt - transform.position;
            if (Vector3.Angle(direction, directionToTarget) < fov / 2f)
            {
                Debug.Log("INSIDE ANGLE");

                //Shoot out raycast to hit target and walls.
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, directionToTarget, viewDistance, layerMask);
                if(raycastHit2D.collider != null)
                {
                    Debug.Log(raycastHit2D.collider.tag);
                    
                    //If the object seen is the target, then switch state, if not, then it is a wall
                    if(raycastHit2D.collider.tag == "Player")
                    {
                        Debug.Log("SPOTTET!!!");

                        //Player spottet!


                        //Change state and execute code for when player is spottet.

                        currentState = State.ChasePlayer;


                    }
                }
            }
                
        }
    }

    //Called when the enemy should check if the target is too far away when chasing.
    private void CheckOutOfChaseRange()
    {
        //If target is too far away. Lose interest and go back to patrolling.
        if (Vector2.Distance(transform.position, target.transform.position) >= chaseDistance)
        {
            currentState = State.Patrol;
        }
    }
}
