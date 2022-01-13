using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrinceController : MonoBehaviour
{
    public float lookRadius = 10f;

    public float StunTime = 1.0f;
    bool isStun = false;
    float currentStunTime = 0.0f;

    public int MaxHealth = 5;
    int CurrentHealth;

    Transform target;
    NavMeshAgent agent;
    Rigidbody rg;

    public Animator animator;

    bool _isFacingRight = true;

    public float AttackRate = 2f;
    float nextAttackTime = 0;
    public Transform AttackPoint;
    public LayerMask playerLayers;
    public float attackRange = 1.0f;

    public int attackDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rg = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Stun check
        if (isStun && currentStunTime < StunTime)
        {
            currentStunTime += Time.deltaTime;
            agent.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            animator.SetBool("Attacked", false);
            isStun = false;
            currentStunTime = 0;
        }

        // look for players
        if (distance <= lookRadius)
        {
            Flip(); // Face to the Player
            agent.SetDestination(target.position);
            if (distance < agent.stoppingDistance && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(agent.velocity.x));
    }


    void Flip()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if (_isFacingRight != (direction.x > 0))
        {
            transform.Rotate(0f, 180f, 0f);
            _isFacingRight = !_isFacingRight;
        }
    }

    void Attack()
    {
        // Debug.Log("DP attack");
        animator.SetTrigger("Attack");

        Collider[] hitPlayers = Physics.OverlapSphere(AttackPoint.position, attackRange, playerLayers);
        foreach (Collider player in hitPlayers)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    void Dash()
    {
        // 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    void Die()
    {
        Debug.Log("DP died");
        animator.SetBool("IsDead", true);
        Destroy(gameObject);
        PlayerController.PlayerInstance.SetKeys();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        animator.SetBool("Attacked", true);
        isStun = true;
    }
}
