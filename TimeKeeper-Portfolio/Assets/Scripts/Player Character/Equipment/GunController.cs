using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimState
{
    Hipfire,
    Aiming
}


public class GunController : MonoBehaviour
{


    public Gun[] GunList;
    public Transform GunInitialPosition;

    private void Start()
    {
        m_GunIndex = 0;

        EquipGun(Instantiate(GunList[m_GunIndex],GunInitialPosition));
        if (EquippedGun)
        {
            EquippedGun.transform.position = GunInitialPosition.position;
            
        }
    }

    private void Update()
    {
        if(m_AimState == AimState.Aiming)
        {
            EquippedGun.Aim();
        }
        else if(m_AimState == AimState.Hipfire)
        {
            EquippedGun.HipFire();
        }
    }

    public void EquipGun(Gun g)
    {
        
        EquippedGun = g;
    }

    public void SwitchGuns()
    {
        m_GunIndex++;
        if(m_GunIndex >= GunList.Length)
        {
            m_GunIndex = 0;
        }

        EquipGun(GunList[m_GunIndex]);
    }

    public void OnTriggerHold()
    {
        EquippedGun.Use();
    }

    public void OnReload()
    {
        EquippedGun.Reload();
    }

    public void ChangeAim(AimState a)
    {
        m_AimState = a;
    }


    public Gun EquippedGun { get; private set; }





    private int m_GunIndex;
    private AimState m_AimState;

 

}
