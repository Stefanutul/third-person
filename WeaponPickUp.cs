using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public RaycastWeapon weaponFab; // weapon prefab

    private void OnTriggerEnter(Collider other)
    {
        Active_Weapon activeWeapon = other.gameObject.GetComponent<Active_Weapon>();
        if(activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(weaponFab);
            activeWeapon.Equip(newWeapon);
        }
    }


    
}
