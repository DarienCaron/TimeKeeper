using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, IEquipment
{

    public EquipmentType EquipmentType;


    public virtual void Equip()
    {
        Debug.Log("Equip");
    }

    public virtual void UnEquip()
    {
        Debug.Log("Unequip");
    }

    public virtual void Use()
    {
        Debug.Log("Use");
    }
}
