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
    public float bounty;
    public float chanceOfBounty;
    public float secondsForBountyToDecay;
    float decayRate;
    
    // Start is called before the first frame update
    void Start()
    {
        // Create our health panel on the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        if (Random.value > chanceOfBounty)
        {
            bounty = 0;
        }
        
        if (secondsForBountyToDecay != 0)
        {
            decayRate = bounty / secondsForBountyToDecay;
        }
        myHealthBar.bountyText.text = bounty.ToString();
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
        if (bounty > 0)
        {
            bounty -= decayRate * Time.deltaTime;
            myHealthBar.bountyText.enabled = true;
            myHealthBar.bountyText.text = BountyAsInt().ToString();
        }
        else
        {
            if (bounty < 0)
            {
                bounty = 0;
            }
            myHealthBar.bountyText.enabled = false;
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
                // This is where we die
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                References.scoreManager.IncreaseScore(BountyAsInt());
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

    int BountyAsInt()
    {
        return Mathf.FloorToInt(bounty);
    }

    private void OnDestroy()
    {
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }
}
