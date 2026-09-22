using UnityEngine;

public class SimpleFSM : MonoBehaviour
{

    // 1. Enum defining all possible FSM states 

    public enum State
    {
        Patrol,
        Chase,
        Attack,
        Flee // Challenge: Newly added extra state 
    }

    [Header("FSM Settings")]
    public State currentState = State.Patrol;


    [Header("Target & Stats")]
    public Transform player; // Drag Player object here
    public float health = 100f;
    public float playerDistance; //auto calculate

    [Header("Ranges")]

    public float attackRange = 2f;
    public float detectRange = 8f;

    void Update()
    {
        // Calculate distance to player

        if (player != null)

        {
            playerDistance = Vector3.Distance(transform.position, player.position);
        }
        // 2. Switch statement handling state execution &amp; transitions
        switch (currentState) 

        {
            
            case State.Patrol:
                PerformPatrol();

                // Transitions from Patrol 
                if (playerDistance <= detectRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:

                PerformChase();

                // Transitions from Chase

                if (health < 20f)
                // Transition into newly added state 

                {
                    currentState = State.Flee;
                }
                else if (playerDistance <= attackRange)
                {
                    currentState = State.Attack;
                }
                else if (playerDistance > detectRange)
                {
                    currentState = State.Patrol;
                }
                break;

            case State.Attack:
                PerformAttack();
                // Transitions from Attack
                if (health < 20f)

                {
                    currentState = State.Flee;
                }
                else if (playerDistance > attackRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Flee:
                // Challenge: Extra state implementation 
                PerformFlee();

                // Transition out of Flee
                if (health >= 50f)
                {
                    currentState = State.Patrol;
                }
                break;
            }
        }

    // --- Behaviors & Basic 3D Movement --- 

    void PerformPatrol()
    {
        Debug.Log("Patrolling area...");
        transform.Rotate(0, 30f * Time.deltaTime, 0);
        // Spins slowly searching 
    }

    void PerformChase()
    {
        Debug.Log("Chasing player...");
        if (player != null)
        {
            transform.LookAt(player.position); // Face player transform.Translate(Vector3.forward * 3f * Time.deltaTime); // Walk toward player 
        }
    }

    void PerformAttack()
    {
        Debug.Log("Attacking player!");

        if (player != null)
        {
            transform.LookAt(player.position); // Stay facing player 
        }

    }
    void PerformFlee()
    {
        Debug.Log("Fleeing to safety...");
        if (player != null)
        {
            // Face opposite direction and run away 
            Vector3 runAwayDir = transform.position + (transform.position - player.position);
            transform.LookAt(runAwayDir); 
            transform.Translate(Vector3.forward * 4f * Time.deltaTime);
        }
    }
}
