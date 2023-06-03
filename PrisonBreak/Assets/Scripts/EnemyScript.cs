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

        //Rotation code

        //State machine for the AI
        switch (currentState)
        {
            default:
                break;

            case State.Patrol:
                //All behavir for the AI to do while patrolling

                agent.destination = PatrolPointList[currentPointIndex].transform.position;

                Debug.Log(PatrolPointList.Count-1);
                Debug.Log("Index: " + currentPointIndex);

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
                }

                break;
            case State.ChasePlayer:
                //All behavior for the AI to do while chasing the player

                break;
            case State.AttackPlayer:
                //All behavior for the AI to do while attacking the player

                break; 
        }

    }
}
