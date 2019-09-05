using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Cross referencing character controller made by Konstantine Titov

public class PlayerController : MonoBehaviour
{

    [Header("Ground Variables")]

    public float MovementAccelerationSpeed = 8.0f;
    public float MaxSpeed = 10.0f;
    public float DecelerationSpeed = 10.0f;

    [Space(45)]

    [Header("Rotation Variables")]

    public float YawRotation = 55.0f;


    [Space(25)]

    [Header("Ground Check Values")]

    public float GroundCheckStartOffsetY = 0.5f;
    public float CheckForGroundRadius = 0.5f;
    public float GroundResolutionOverlap = 0.05f;

    public float MinAllowedSurfaceAngle = 15.0f;

    public GameObject FootLocation;

    [Space(35)]
    [Header("Constants")]

    public const float GroundHitAdjustment = 0.1f;

  

    void Start()
    {

        m_GroundCheckMask = ~LayerMask.GetMask("Player", "Ignore Raycast");

        m_RigidBody = GetComponent<Rigidbody>();

        m_Velocity = Vector3.zero;

        m_PlayerEyes = GetComponentInChildren<PlayerLook>();


    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        m_Velocity = m_RigidBody.velocity;

        UpdateGroundInfo();

        Vector3 localMoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        localMoveDir.Normalize();

        UpdateOnGround(localMoveDir);
        RotatePlayer();










    }

    private void RotatePlayer()
    {
        m_RigidBody.MoveRotation(m_RigidBody.rotation * Quaternion.Euler(m_PlayerEyes.BodyRotation));
    }

 

    void UpdateGroundInfo()
    {
        GroundAngularVelocity = Vector3.zero;
        GroundVelocity = Vector3.zero;
        GroundNormal = Vector3.forward;

        m_CenterHeight = transform.position.y;

        float footHeight = FootLocation.transform.position.y;
        float halfCapsuleHeight = m_CenterHeight - footHeight;

        Vector3 rayStart = transform.position;
        rayStart.y += GroundCheckStartOffsetY;

        Vector3 rayDir = Vector3.down;

        float rayDist = halfCapsuleHeight + GroundCheckStartOffsetY - CheckForGroundRadius;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, CheckForGroundRadius, rayDir, rayDist + GroundHitAdjustment, m_GroundCheckMask);

        RaycastHit groundHitInfo = new RaycastHit();

        bool validGroundFound = false;

        float minGroundDist = float.MaxValue;

        foreach(RaycastHit hits in hitInfos)
        {

            // Replace the math equation below with a helper function. This is to calculate the vertical angle.
            float surfaceAngle = MathUtilities.CalculateVerticalAngle(hits.normal);

            if(surfaceAngle < MinAllowedSurfaceAngle || hits.distance <= 0.0f)
            {
                continue;
            }
            if(hits.distance < minGroundDist)
            {
                minGroundDist = hits.distance;
                groundHitInfo = hits;
                validGroundFound = true;
            }

        }

        if(!validGroundFound)
        {
            // change this for when the player is in the air.
            return;
        }

        Vector3 bottomAtHitPoint = MathUtilities.ProjectToBottomOfCapsule(groundHitInfo.point, transform.position, halfCapsuleHeight * 2.0f, CheckForGroundRadius);

        float stepUpAmount = groundHitInfo.point.y - bottomAtHitPoint.y;
        m_CenterHeight += stepUpAmount - GroundResolutionOverlap;

        GroundNormal = groundHitInfo.normal;
        
    }

    void UpdateOnGround(Vector3 localMoveDir)
    {
        if(localMoveDir.sqrMagnitude > MathUtilities.CompareEpsilon)
        {
            Vector3 localVelocity = m_Velocity - GroundVelocity;

            Vector3 moveAccel = CalculateMoveAccel(localMoveDir);
            Vector3 groundTangent = moveAccel - Vector3.Project(moveAccel, GroundNormal);

            groundTangent.Normalize();

            moveAccel = groundTangent;

            Vector3 velocityAlongMoveDir = Vector3.Project(localVelocity, moveAccel);


            if(Vector3.Dot(velocityAlongMoveDir, moveAccel) > 0.0f)
            {
                localVelocity = MathUtilities.LerpTo(DecelerationSpeed, localVelocity, velocityAlongMoveDir, Time.fixedDeltaTime);

            }
            else
            {
                localVelocity = MathUtilities.LerpTo(DecelerationSpeed, localVelocity, Vector3.zero, Time.fixedDeltaTime);
            }

            moveAccel *= MovementAccelerationSpeed;
            localVelocity += moveAccel * Time.fixedDeltaTime;
            localVelocity = Vector3.ClampMagnitude(localVelocity, MaxSpeed);

            m_Velocity = localVelocity + GroundVelocity;

        }
        else
        {
            UpdateStopping(DecelerationSpeed);
        }

        ApplyVelocity(m_Velocity);

        Vector3 playerCenter = transform.position;


        playerCenter.y = MathUtilities.LerpTo(DecelerationSpeed, playerCenter.y, m_CenterHeight, Time.deltaTime);

        transform.position = playerCenter;


    }


    void UpdateStopping(float stopEaseSpeed)
    {
        m_Velocity = MathUtilities.LerpTo(stopEaseSpeed, m_Velocity, GroundVelocity, Time.fixedDeltaTime);
    }

    void ApplyVelocity(Vector3 velocity)
    {
        Vector3 velocityDiff = velocity - m_RigidBody.velocity;
        m_RigidBody.AddForce(velocityDiff, ForceMode.VelocityChange);
    }




    Vector3 CalculateMoveAccel(Vector3 localMoveDir)
    {
        Vector3 moveAccel = localMoveDir;
        moveAccel = transform.TransformDirection(moveAccel);
        return moveAccel;
    }








    public Vector3 GroundVelocity { get; private set; }
    public Vector3 GroundAngularVelocity { get; private set; }
    public Vector3 GroundNormal { get; private set; }





    Rigidbody m_RigidBody;
    Vector3 m_Velocity;
    float m_CenterHeight;
    int m_GroundCheckMask;
    PlayerLook m_PlayerEyes;




    bool m_IsCrouching; // Temp


}
