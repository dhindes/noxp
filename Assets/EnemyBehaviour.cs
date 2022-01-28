using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    protected Rigidbody ourRigidBody;
    public NavMeshAgent myNavAgent;

    // This is our parent class for enemies - code here should be stuff they all use

    // Protected: this function can be used by our children, and us - but no one else. Put this in front of
        // everything our children might want to use

    // Virtual: this can be overridden by our children, but if they don't, they use our version
    // Void: returns nothing

    // Protected: children can use this function
    // Virtual: children can override this function

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        myNavAgent   = GetComponent<NavMeshAgent>();
        myNavAgent.speed = speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void OnEnable()
    {
        References.allEnemies.Add(this);
    }

    protected void OnDisable()
    {
        References.allEnemies.Remove(this);
    }

    protected virtual void ChasePlayer()
    {
        if (References.thePlayer != null)
        {
            myNavAgent.destination = References.thePlayer.transform.position;
            
            /*Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;

            // Follow the player
            ourRigidBody.velocity = vectorToPlayer.normalized * speed;
            Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
            transform.LookAt(playerPositionAtOurHeight);*/
        }
    }
}
