using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float secondsToExist;
    float secondsWeveBeenAlive;
    public GameObject soundObject;
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        secondsWeveBeenAlive = 0;
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float lifeFraction = secondsWeveBeenAlive / secondsToExist;
        Vector3 maxScale = Vector3.one * 5;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction);
        
        secondsWeveBeenAlive += Time.fixedDeltaTime;
        if (secondsWeveBeenAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Look for a health system on the thing we collided with
        HealthSystem theirHealthSystem = collision.gameObject.GetComponentInParent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            // If we found one, do a lot of damage
            theirHealthSystem.TakeDamage(damage);
        }
    }
}
