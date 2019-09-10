using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipment
{

    public float MaxDamage = 10f;











    public virtual void Equip()
    {
        Debug.Log(gameObject.name + "Equipped");
    }

    public virtual void UnEquip()
    {
        Debug.Log(gameObject.name + "Unequipped");
    }

    public virtual void Use()
    {
        Debug.Log("Using" + gameObject.name);
    }

   
}
