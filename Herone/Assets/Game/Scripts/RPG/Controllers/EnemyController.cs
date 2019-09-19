using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour
{

    public float lookRadius;    // Detection range for player
    public Transform target;    // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    CharacterCombat combat;
    public bool fighting;
    public Collider[] colliders;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        agent.stoppingDistance = GetComponent<EnemyStats>().rangeAttack;
    }

    // Update is called once per frame
    void Update()
    {        
        
        if (!fighting)
        {
            SearchPlayer();
        }

        // Distance to the target
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            // If inside the lookRadius
            if (distance <= lookRadius)
            {
                // Move towards the target
                agent.SetDestination(target.position);

                // If within attacking distance
                if (distance <= agent.stoppingDistance)
                {
                    
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();

                    if (targetStats != null)
                    {
                        if (GetComponent<EnemyStats>().ranged)
                        {
                            combat.AttackRanged(targetStats);
                        }
                        else
                        {
                            combat.Attack(targetStats);
                        }
                    }
                    FaceTarget();   // Make sure to face towards the target                    
                }
            }
            else
            {
                fighting = false;
                target = null;
                agent.SetDestination(transform.position);
            }

        }

    }
    void SearchPlayer()
    {

        colliders = Physics.OverlapSphere(transform.position, lookRadius);

        foreach (Collider col in colliders)
        {
            if (col.tag == "Player")
            {
                fighting = true;
                target = col.transform;
                return;
            }
            else
            {
                fighting = false;
            }
        }
    }
    // Rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
