using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : BulletBehaviour
{
    public LineRenderer myBeam;

    // Start is called before the first frame update
    void Start()
    {
        // Fire a laser to see how far we can go before we hit a wall
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            out RaycastHit hitInfo,
                            References.maxDistanceInALevel,
                            References.wallsLayer))
        {
            float distanceToWall = hitInfo.distance;

            myBeam.SetPosition(0, transform.position);
            myBeam.SetPosition(1, hitInfo.point);

            // For each individual thing - of data type RaycastHit - in this RaycastAll(), name it
            // enemyHitInfo, then iterate through each enemyHitInfo and do what's in the braces
            foreach (RaycastHit enemyHitInfo in Physics.SphereCastAll(transform.position,
                                                                      0.3f,
                                                                      transform.forward,
                                                                      distanceToWall,
                                                                      References.enemiesLayer))
            {
                //Get each collider   // Get Healt System in each parent    // Take damage
                enemyHitInfo.collider?.GetComponentInParent<HealthSystem>()?.TakeDamage(damage);
            }
        }
    }

    protected override void Update()
    {
        base.Update();  //This will handle our lifetime countdown timer

        // Make our beam fade out over time
        myBeam.endColor = Color.Lerp(myBeam.endColor, Color.clear, 0.01f);

    }
}
