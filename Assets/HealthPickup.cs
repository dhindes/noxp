using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void ReplenishHealth()
    {
        References.thePlayer?.GetComponent<HealthSystem>()?.ReplenishHealth();
        References.alarmManager?.RaiseAlertLevel();
        Destroy(gameObject);
    }
}
