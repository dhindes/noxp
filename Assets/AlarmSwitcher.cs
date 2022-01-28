using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSwitcher : MonoBehaviour
{
    public GameObject unalarmedObject;
    public GameObject alarmedObject;

    // Update is called once per frame
    void Update()
    {
        if (unalarmedObject.activeInHierarchy 
            && References.alarmManager.AlarmHasSounded())
        {
            unalarmedObject.SetActive(false);
            alarmedObject.SetActive(true);
        }
    }
}
