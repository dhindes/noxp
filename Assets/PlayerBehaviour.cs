using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;

    public WeaponBehaviour mainWeapon;
    public WeaponBehaviour secondaryWeapon;

    private void Awake()
    {
        References.thePlayer = this;
    }
    private void Start()
    {
        if (mainWeapon != null)
        {
            References.canvas.mainWeaponPanel.AssignWeapon(mainWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // WASD to move
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        // Aim at cursor
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPane = new Plane(Vector3.up, transform.position);
        playerPane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

        // Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        // Click to fire
        if (mainWeapon != null && Input.GetButton("Fire1"))
        {
            // Tell our weapon to fire
            mainWeapon.Fire(cursorPosition);
        }

        // Press to swap weapons
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapons();
        }

        // Use the nearest usable
        Usable nearestUsableSoFar = null;
        float nearestDistance = 3;      // Maximum pickup distance
        foreach (Usable thisUsable in References.usables)
        {
            // How far is this usable from the player?
            float thisDistance = Vector3.Distance(transform.position,
                                                  thisUsable.transform.position);
            // If it's closer than anything else we've found,
            // make it the closest usable
            if (thisDistance <= nearestDistance)
            {
                nearestUsableSoFar = thisUsable;
                nearestDistance = thisDistance;
            }
        }

        // After finding the closest usable, use it
        if (nearestUsableSoFar != null)
        {
            // If something is in use range, show the Use prompt
            References.canvas.usePromptSignal = true;
            if (Input.GetButtonDown("Use"))
            {
                nearestUsableSoFar?.Use();
            }
        }
    }

    private void OnDestroy()
    {
        References.scoreManager.UpdateHighScore();
    }

    // Swap weapons
    private void SwitchWeapons()
    {
        if (mainWeapon != null && secondaryWeapon != null)
        {
            WeaponBehaviour currentMainWeapon = mainWeapon;
            WeaponBehaviour currentSecondaryWeapon = secondaryWeapon;
            SetAsMainWeapon(currentSecondaryWeapon);
            SetAsSecondaryWeapon(currentMainWeapon);
        }

        //weapons[i].gameObject.SetActive(true);  // Activate (tick) the current weapon
        //weapons[i].gameObject.SetActive(false); // Deactivate (untick) all other weapons
    }

    public void PickUpWeapon(WeaponBehaviour weapon)
    {
        if (mainWeapon == null)
        {
            // If we don't have a main weapon, use this new weapon now
            SetAsMainWeapon(weapon);
        }
        else
        {
            // If we do have a main weapon, put this into our secondary weapon slot
            if (secondaryWeapon == null)
            {
                SetAsSecondaryWeapon(weapon);
            }
            else
            {
                // If both weapon slots are full, drop main weapon and pick this up as main weapon
                mainWeapon.Drop();
                SetAsMainWeapon(weapon);
            }
        }
    }

    private void SetAsMainWeapon(WeaponBehaviour weapon)
    {
        mainWeapon = weapon;
        References.canvas.mainWeaponPanel?.AssignWeapon(mainWeapon);    // Show weapon ammo on UI
        weapon.gameObject.SetActive(true);
    }
    private void SetAsSecondaryWeapon(WeaponBehaviour weapon)
    {
        secondaryWeapon = weapon;
        References.canvas.secondaryWeaponPanel?.AssignWeapon(secondaryWeapon);    // Show weapon ammo on UI
        weapon.gameObject.SetActive(false);
    }
}
