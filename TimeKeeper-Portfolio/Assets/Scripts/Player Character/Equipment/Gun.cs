using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon, IShootable
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


    [Header("Recoil")]
    public Vector2 KickbackMinMax;

    public Vector2 RecoilMinMax;



    void Start()
    {
        m_OriginalHipFirePos = transform.localPosition;
        Reload();
        Parent = GetComponentInParent<PlayerController>().gameObject;
        m_RecoilAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = CalculateCenterDir();
        
        transform.rotation = Quaternion.LookRotation(Direction, transform.up);

        
    }

    private void LateUpdate()
    {
        m_RecoilAngle = Mathf.SmoothDamp(m_RecoilAngle, 0, ref m_RecoilRotSmoothDampVelocity, 0.1f);
        transform.localEulerAngles = transform.localEulerAngles + Vector3.left * m_RecoilAngle;
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
        m_ShotCounter++;



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
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.GetComponent<Bullet>().Init(MuzzleLocation.position, direction);
            CurrentAmmoCount--;
        }

    

        CalculateKickBack(KickbackMinMax.x, KickbackMinMax.y);
        m_RecoilAngle += 5;
        m_RecoilAngle = Mathf.Clamp(m_RecoilAngle, RecoilMinMax.x, RecoilMinMax.y); 
    }

    protected void CalculateKickBack(float min, float max)
    {
        float random = Random.Range(min, max);    

        transform.localPosition -= Vector3.forward * random;
    }

    public void Aim()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, ADSLocation, Time.deltaTime * ADSSpeed);
    }

    public void HipFire()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, m_OriginalHipFirePos, Time.deltaTime * ADSSpeed);
    }

    Vector3 CalculateCenterDir()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 direction = ray.direction;
        direction.Normalize();

        return direction;
    }

    private float m_RecoilRotSmoothDampVelocity;

    protected bool m_IsReloading;
    private Vector3 m_OriginalHipFirePos;
    private int m_ShotCounter;
    private float m_RecoilAngle;
    private float m_Timer;

   

}
