using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //
    [SerializeField] private List<GameObject> PatrolPointList = new List<GameObject>();
    private int currentPointIndex;


    void Start()
    {
        //Initializing agent variables necessary for the agent to work in 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentPointIndex = 0;
        currentSpeed = 3.5f;
        viewDistance = 15f;

        lookAt = PatrolPointList[currentPointIndex].transform.position;
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
                agent.destination = PatrolPointList[currentPointIndex].transform.position;
                if (Vector2.Distance(transform.position, PatrolPointList[currentPointIndex].transform.position) < 0.5)
                {
                    if(currentPointIndex >= PatrolPointList.Count-1)
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
        //Check if target if inside view distance of the enemy
        if(Vector2.Distance(transform.position, target.transform.position) <= viewDistance)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            var direction = lookAt - transform.position;
        }
    }
}
