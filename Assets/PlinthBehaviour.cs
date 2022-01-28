using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlinthBehaviour : MonoBehaviour
{
    Usable myUsable;
    public TextMeshProUGUI myLabel;
    public Transform spotForItem;
    public GameObject cage;
    public float secondsToLock;

    private void OnEnable()
    {
        References.plinths.Add(this);
    }

    private void OnDisable()
    {
        References.plinths.Remove(this);
    }

    public void AssignItem(GameObject item)
    {
        myUsable = item.GetComponent<Usable>();
        myLabel.text = myUsable.displayName;
        myUsable.transform.position = spotForItem.transform.position;
        myUsable.transform.rotation = spotForItem.transform.rotation;
    }

    private void Update()
    {
        if (myUsable != null && !myUsable.enabled)
        {
            myUsable = null;
        }
        if (secondsToLock > 0 && References.alarmManager.AlarmHasSounded())
        {
            secondsToLock -= Time.deltaTime;
            if (secondsToLock <= 0)
            {
                cage.SetActive(true);
                myLabel.text = "ALARM";
                // If our object still exists, destroy it so it can't be taken
                if (myUsable != null && myUsable.enabled)
                {
                    Destroy(myUsable.gameObject);
                }
            }
        }
    }
}
