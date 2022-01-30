using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiqueBehaviour : MonoBehaviour
{
    public void BeCollected()
    {
        References.scoreManager?.IncreaseScore(20);
        References.alarmManager?.RaiseAlertLevel();
        Destroy(gameObject);
    }
}
