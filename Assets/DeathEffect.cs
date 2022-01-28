using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float shakeAmount;
    AudioSource myAudioSource;
    public Light myLight;
    float maxLightIntensity;
    public float duration;
    float secondsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        References.cameraTools.shakeAmount = shakeAmount;
        myAudioSource = GetComponent<AudioSource>();
        maxLightIntensity = myLight.intensity;
        secondsRemaining = duration;
    }

    // Update is called once per frame
    void Update()
    {
        myLight.intensity = (secondsRemaining / duration) * maxLightIntensity;
        secondsRemaining -= Time.deltaTime;
        if (secondsRemaining <= 0)
        {
            if (!myAudioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
