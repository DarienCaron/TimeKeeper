using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{

    public float MaxDamage = 10f;

    




    public override void Equip()
    {
        Debug.Log(gameObject.name + "Equipped");
    }

    public override void UnEquip()
    {
        Debug.Log(gameObject.name + "Unequipped");
    }

    public override void Use()
    {
        Debug.Log("Using" + gameObject.name);
    }


   
}
