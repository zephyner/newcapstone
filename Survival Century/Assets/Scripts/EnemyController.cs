using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject projectile;

    public LayerMask theGround;
    public LayerMask thePlayer;

    //This is for the enemies walking and patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;

    //This is for attacking
    public float attackTime;
    bool alreadyAttacked;

    /*This is for the state the enemy goes into, 
     * depending on the player distance*/
    public float seeingRange;
    public float attackRange;
    public bool playerInSight;
    public bool playerInAttackRange;

    public float health = 100;
    bool isDead;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //This will check for the range of following and attacking
        playerInSight = Physics.CheckSphere(transform.position, seeingRange, thePlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);
        
        //For when the player isn't close enough to the enemy
        if (!playerInSight && !playerInAttackRange) Walking();
        //For when the player is in the enemy sight, but not in attack range
        if (playerInSight && !playerInAttackRange) Chase();
        //For when the player is withing the enemy's attack range
        if (playerInSight && playerInAttackRange) Attacking();
    }

    void Walking()
    {
        //Looks for the player
        if (!walkPointSet) WalkPointSearch();
        //Goes to the player
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 walkDistance = transform.position - walkPoint;

        //When the player is out of range
        if (walkDistance.magnitude < 1f)
            walkPointSet = false;
    }

    void WalkPointSearch()
    {
        //Makes a random point on the Z axis in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        //Makes a random point on the X axis in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //checks if the point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, theGround))
            walkPointSet = true;

    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attacking()
    {
        //Makes sure the enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
