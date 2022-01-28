using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehaviour : EnemyBehaviour
{
    public GameObject explosionPrefab;
    
    protected void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        PlayerBehaviour possiblePlayer = theirGameObject.GetComponent<PlayerBehaviour>();

        //if (theirGameObject == References.thePlayer.gameObject)
        if (possiblePlayer == References.thePlayer)
        {
            //thisCollision.gameObject.GetComponent<HealthSystem>()?.TakeDamage(1);
            
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
