using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    Vector3 normalCameraPosition;
    Vector3 desiredCameraPosition;
    public Vector3 joltVector;
    public float joltDecayFactor;
    public float maxMoveSpeed;
    public float shakeAmount;
    public float shakeDecayFactor;
    public Vector3 cameraOffset;

    private void Awake()
    {
        References.cameraTools = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalCameraPosition = transform.position;
        // Store our position relative to the player
        cameraOffset = transform.position - References.thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Set our position by looking at the player's position and adding our offset
        if (References.thePlayer != null)
        {
            normalCameraPosition = References.thePlayer.transform.position + cameraOffset;
        }
        
        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), 
                                          GetRandomShakeAmount(), 
                                          GetRandomShakeAmount());
        desiredCameraPosition = normalCameraPosition + joltVector + shakeVector;

        // Set our camera to the jolted position
        transform.position = Vector3.MoveTowards(transform.position,
                                                 desiredCameraPosition, 
                                                 maxMoveSpeed * Time.deltaTime);

        // Decrease the jolt vector over time so camera return to normal position
        joltVector *= joltDecayFactor;
        shakeAmount *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
