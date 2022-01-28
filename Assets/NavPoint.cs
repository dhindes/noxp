using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        References.navPoints.Add(this);
    }

    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
