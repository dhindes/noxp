using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponBehaviour : MonoBehaviour
{
    public float accuracy;
    public float numberOfProjectiles;
    public float secondsBetweenShots;
    private float secondsSinceLastShot;
    public float kickAmount;
    public GameObject bulletPrefab;
    public AudioSource myAudioSource;
    public int ammo;
    public int currentAmmo;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
        myAudioSource = GetComponent<AudioSource>();
    }

    public void BePickedUpByPlayer()
    {
        transform.position = References.thePlayer.transform.position;   // Move our weapon model to the player
        transform.rotation = References.thePlayer.transform.rotation;
        transform.SetParent(References.thePlayer.transform);            // Parent us to the player
        References.alarmManager?.RaiseAlertLevel();                     // Raise the alarm level
        References.thePlayer.PickUpWeapon(this);
    }

    public void Fire(Vector3 targetPosition)
    {
        // Click to fire
        // If clicked, create a bullet at our current position
        if (secondsSinceLastShot >= secondsBetweenShots 
            && currentAmmo > 0)
        {
            // Ready to fire
            References.alarmManager.SoundTheAlarm();
            myAudioSource.Play();
            References.cameraTools.joltVector = transform.forward * kickAmount;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                newBullet.GetComponent<BulletBehaviour>().damage = damage;
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
                Vector3 newTargetPosition = targetPosition;
                newTargetPosition.x += Random.Range(-inaccuracy, inaccuracy);
                newTargetPosition.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(newTargetPosition);
                newBullet.name = i.ToString();
                secondsSinceLastShot = 0;
            }

            currentAmmo--;
        }
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Usable>().enabled = true;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}
