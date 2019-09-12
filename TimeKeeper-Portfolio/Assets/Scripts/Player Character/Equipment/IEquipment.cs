using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEquipment 
{
    void Equip();
    void UnEquip();
    void Use();

}

public enum EquipmentType
{
    Weapon,
    Tool,
    Gun
};
