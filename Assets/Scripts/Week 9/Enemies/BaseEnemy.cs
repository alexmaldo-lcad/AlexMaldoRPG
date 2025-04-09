using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;
    public float attackDamage = 0f;
    public float attackRange;

    private float timer = 0f;
    protected PlayerRPG player;
    protected NavMeshAgent navAgent;

    [SerializeField]
    protected float aggroRange = 30f;

    protected bool hasSeenPlayer = false;

    [SerializeField]
    protected float attackInterval = 1f;

    [SerializeField]
    protected List<Transform> patrolPoints = new List <Transform>();

    protected int patrolPointIndex = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRPG>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(patrolPoints[patrolPointIndex].position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (hasSeenPlayer == true)
        {
            if (navAgent.remainingDistance < 0.5f) //has reached  LAST KNOWN location of player
            {
                if (Vector3.Distance(this.transform.position, player.transform.position) > aggroRange)
                {
                    hasSeenPlayer = false;

                }
                else //if they r not out of aggro range
                {
                    if (IsPlayerInLoS() == true)
                    {
                        navAgent.SetDestination(player.transform.position);
                        navAgent.isStopped = false;
                    }
                    else
                    {
                        hasSeenPlayer = false;
                    }
                }
            }
            //if player in atk range
            if (Vector3.Distance(this.transform.position, player.transform.position) > attackRange)
            {
                if (IsPlayerInLoS() == true) //n in LoS
                {
                    navAgent.SetDestination(player.transform.position);
                    navAgent.isStopped = false;
                }
            }
            else //player IS in atk range
            {
                if (IsPlayerInLoS() == true) //n in LoS
                {
                    navAgent.isStopped = true;

                    this.transform.LookAt(player.transform.position);
                    timer += Time.deltaTime;

                    if (timer >= attackInterval)
                    {
                        Attack();
                        timer = 0f;
                    }

                }
                else
                {
                    navAgent.isStopped = false;
                }
            }
        }
        else //player has not been seen
        {
            if(navAgent.remainingDistance < 0.5f)
            {
                patrolPointIndex++;

                if(patrolPointIndex >= patrolPoints.Count)
                {
                    patrolPointIndex = 0;
                }

                navAgent.SetDestination(patrolPoints[patrolPointIndex].position);
                navAgent.isStopped = false;
            }
        }
    }

    protected bool IsPlayerInLoS()
    {
        RaycastHit hit;

        Vector3 dir = player.transform.position - this.transform.position;
        dir.Normalize();

        if (Physics.Raycast(this.transform.position, dir, out hit))
        {

            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void Attack()
    {
        player.TakeDamage(attackDamage);
    }
    public virtual void EnemyTakeDamage(int damageTaken)
    {
        health -= damageTaken;

        if(health <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public void SeePlayer()
    {
       if(IsPlayerInLoS() == true)
       {
            hasSeenPlayer = true;
       }
    }
}
