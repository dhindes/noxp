using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")] // Write this to tell Unity not to lose our data when we rename a variable
    public float maxHealth;
    float currentHealth;
    public GameObject healthBarPrefab;
    public GameObject deathEffectPrefab;
    HealthBar myHealthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        // Create our health panel on the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Make our health bar show our health status
        //myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        // Make our health bar follow us - move it to our current position
        if (myHealthBar != null)
        {
            myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        }
        
    }
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
        {

            currentHealth -= damageAmount;
            myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
            if (currentHealth <= 0)
            {
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }

    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
    }

    private void OnDestroy()
    {
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }
}
