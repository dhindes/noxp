using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public void ReplenishAmmo()
    {
        RefillWeapon(References.thePlayer.mainWeapon);
        RefillWeapon(References.thePlayer.secondaryWeapon);
        References.alarmManager?.RaiseAlertLevel();
        Destroy(gameObject);
    }

    void RefillWeapon(WeaponBehaviour weapon)
    {
        if (weapon != null)
        {
            weapon.currentAmmo = weapon.ammo;
        }
    }
}
