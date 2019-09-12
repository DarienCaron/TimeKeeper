using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{


    public Transform MuzzleLocation;
    public GameObject BulletPrefab;
    public GameObject Parent;

    public float FireDistance = 55.0f;

    public float FireRate = 1;
    public int MaxAmmoCount = 15;


    public int CurrentAmmoCount { get; private set; }

    public Vector3 ADSLocation = new Vector3(-0.008f, 0.116f, 0.421f);
    public float ADSSpeed = 15f;


    void Start()
    {
        
        Reload();
        Parent = GetComponentInParent<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {     
      
    }

    public virtual void Reload()
    {
        CurrentAmmoCount = MaxAmmoCount;
    }

    public override void Equip()
    {
        base.Equip();
    }
    public override void UnEquip()
    {
        base.UnEquip();
    }

    public override void Use()
    {
        if (CurrentAmmoCount > 0)
        {
            Fire();
        }
    }


    void Fire()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        RaycastHit hit;


        Vector3 direction = Vector3.zero;

        Quaternion rot = Quaternion.identity;

        if(Physics.Raycast(ray, out hit))
        {
            direction = hit.point - MuzzleLocation.position;
            direction.Normalize();
        
        }
        else
        {
            direction = ray.direction;
        }

        

        rot = Quaternion.LookRotation(direction);

        if (Parent)
        {
            GameObject bullet = Instantiate(BulletPrefab, MuzzleLocation.position, rot);
            CurrentAmmoCount--;
        }

    }

    protected bool m_IsReloading;
}
