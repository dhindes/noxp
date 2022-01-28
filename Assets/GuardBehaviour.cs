using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : EnemyBehaviour
{
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public Light myLight;
    public float turnSpeed;
    public WeaponBehaviour myWeapon;
    public float reactionTime;
    float secondsSeeingPlayer;

    // This is specifically Guard behaviour - generic enemy behaviour is handled by parent, EnemyBehaviour

    // We have to give things the same access type as the parent
    // Protected parent classes must be protected here

    // Override: We know our parent has this, and we're going to use our version instead
    // Add 'base.ClassName() to run the parent's version of this first, before ours

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        alerted = false;
        ourRigidBody = GetComponent<Rigidbody>();
        GoToRandomNavPoint();
        secondsSeeingPlayer = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (References.alarmManager.AlarmHasSounded())
        {
            alerted = true;
        }
        
        if (References.thePlayer != null)
        {
            Vector3 playerPosition = GetPlayerPosition(); //References.thePlayer.transform.position;
            Vector3 vectorToPlayer = GetVectorToPlayer(); //playerPosition - transform.position;
            myLight.color = Color.white;

            if (alerted)
            {
                myLight.color = Color.red;
                ChasePlayer();
                if (CanSeePlayer())
                {
                    secondsSeeingPlayer += Time.deltaTime;
                    // transform.LookAt(PlayerPosition());
                    if (secondsSeeingPlayer >= reactionTime)
                    {
                        myWeapon.Fire(playerPosition);
                    }
                }
                else
                {
                    secondsSeeingPlayer = 0;
                }
            }
            else
            {
                if (myNavAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

                ourRigidBody.velocity = transform.forward * speed;

                // Checking if we can see the player
                if (Vector3.Distance(transform.position, playerPosition) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        //Check for walls - starting point, direction, distance to check, layer of objects to check
                        if (!Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer))
                        {
                            alerted = true;
                            References.alarmManager.SoundTheAlarm();
                            myLight.color = Color.red;
                        }
                    }
                }
            }
        }
    }

    public void KnockoutAttempt()
    {
        if (!References.alarmManager.AlarmHasSounded())
        {
            GetComponent<HealthSystem>()?.KillMe();
            References.alarmManager?.RaiseAlertLevel();
        }
        
    }

    void GoToRandomNavPoint()
    {
        int randomNavPointIndex = Random.Range(0, References.navPoints.Count);
        myNavAgent.destination = References.navPoints[randomNavPointIndex].transform.position;

    }

    protected Vector3 GetPlayerPosition()
    {
        return References.thePlayer.transform.position;
    }

    protected Vector3 GetVectorToPlayer()
    {
        return GetPlayerPosition() - transform.position;
    }

    protected bool CanSeePlayer()
    {
        if (References.thePlayer == null)
        {
            return false;
        }

        Vector3 vectorToPlayer = GetVectorToPlayer(); //playerPosition - transform.position;

        if (!Physics.Raycast(transform.position,
                           vectorToPlayer,
                           vectorToPlayer.magnitude,
                           References.wallsLayer)
            && (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle))
        {
            return true;
        }
        return false;
    }
}
