using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrinceController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    bool _isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            Flip(); // Face to the Player
            agent.SetDestination(target.position);
            if(distance < agent.stoppingDistance)
            {
                //Attack
            }
        }

    }

    void Flip()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if(_isFacingRight != (direction.x > 0))
        {
            transform.Rotate(0f, 180f, 0f);
            _isFacingRight = !_isFacingRight;
        }
        Debug.Log("("+direction.x+","+direction.y+")");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Die()
    {
        //Death animation
        Destroy(gameObject);
    }
}
