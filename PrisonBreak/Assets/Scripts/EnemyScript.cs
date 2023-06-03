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
    [SerializeField] private State currentState;

    [Tooltip("The target of the enemy. This will usually be the player object")]
    public Transform target;

    private float currentSpeed;
    private float walkSpeed;
    private float chaseSpeed;

    private NavMeshAgent agent;

    private float viewDistance;

    //Variable for where the enemy should look
    private Vector3 lookAt;
    private float fieldOfView;

    //Variables for patrol points
    [SerializeField] private List<GameObject> PatrolPointList = new List<GameObject>();
    private int currentPointIndex;

    [SerializeField] private LayerMask layerMask;


    void Start()
    {
        //Initializing agent variables necessary for the agent to work in 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentPointIndex = 0;
        currentSpeed = 3.5f;
        viewDistance = 5f;

        if(PatrolPointList.Count != 0)
        {
            lookAt = PatrolPointList[currentPointIndex].transform.position;
        }


        fieldOfView = 90f;
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

                Debug.Log(PatrolPointList.Count);

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

                CheckForTarget();

                //If player spotted switch to chase

                break;
            case State.ChasePlayer:
                //All behavior for the AI to do while chasing the player

                break;
            case State.AttackPlayer:
                //All behavior for the AI to do while attacking the player

                break; 
        }

        

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
            if (Vector3.Angle(direction, directionToTarget) < fieldOfView / 2f)
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
                        //Player spottet!

                        //Change state
                        Debug.Log("SPOTTET!!!");
                    }
                }
            }
                
        }
    }
}
