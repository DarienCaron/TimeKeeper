using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ITimeAffected
{
    [Header("Speed And Lifetime")]
    public float BulletSpeed = 45.0f;
    public float BulletLifetime = 8.0f;

    public float MaxDropOff = 6000;
    public float MinDropOff = 200;

    [Space(25)]
    [Header("Damage")]

    public float MinimumDamage = 10.0f;
    public float MaximumDamage = 45.0f;

    public float DamageDropOffOverTime = 0.25f;

    public float RangeModifier = 0.35f;










    public void Init(Vector3 position, Vector3 velocity)
    {
        m_FiredLocation = position;
        transform.position = m_FiredLocation;
        m_Velocity = velocity;
        CurrentBulletDamage = MaximumDamage;
        m_TimeTillDestroy = BulletLifetime;
    }

    void Update()
    {
     



    }

    private void CalculateDamageDrop(float distance)
    {
        if (distance < MinDropOff)
        {
            CurrentBulletDamage = MaximumDamage;
            return;
        }
        if (distance > MaxDropOff)
        {
            CurrentBulletDamage = MinimumDamage;
            return;
        }


        float Range = MaxDropOff - MinDropOff;

        float distanceNormalized = Mathf.Abs((distance - MinDropOff) / Range) * RangeModifier;

        CurrentBulletDamage = Mathf.Lerp(CurrentBulletDamage, MinimumDamage, distanceNormalized * Time.deltaTime);

       

    }


    private void OnCollisionEnter(Collision collision)
    {     
        Destroy(gameObject);
        Debug.Log("Damage dealt: " + CurrentBulletDamage);
    }

    public void TimedUpdate()
    {
        transform.position += (m_Velocity * BulletSpeed * Time.deltaTime) * TimeWorld.Instance.TimeScale;


        m_TimeTillDestroy -= Time.deltaTime;

        if (m_TimeTillDestroy <= 0.0f)
        {
            Destroy(gameObject);
        }


        m_DistanceFallOff = Vector3.Distance(transform.position, m_FiredLocation);


        CalculateDamageDrop(m_DistanceFallOff);
    }

    public float CurrentBulletDamage { get; private set; }


    private Vector3 m_Velocity;
    private Vector3 m_FiredLocation;
    private float m_TimeTillDestroy;
    private float m_DistanceFallOff;
}
