using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtilities 
{
    public static float CompareEpsilon = 0.00001f;

    private static float HALFVALUE = 0.5f;
    // This function will project a point inside a capsile to the bottom of the capsule.
    // The capsule is assumed to be oriented along the y-axis.
    public static Vector3 ProjectToBottomOfCapsule(Vector3 pointToProject, Vector3 capsuleCenter, float capsuleHeight, float capsuleRadius )
    {
        // Calculating the length of the line segment part of the capsule.
       
        float lineSegmentLength = capsuleHeight - 2.0f * capsuleRadius;

        //Clamp line segment length
        lineSegmentLength = Mathf.Max(lineSegmentLength, 0.0f);

        // Calculate the line segment that goes along the capsules "Height"
        Vector3 bottomLineSegPt = capsuleCenter;
        bottomLineSegPt.y -= lineSegmentLength * HALFVALUE;

        // Get displacement from bottom of line segment
        Vector3 ptDisplacement = pointToProject - bottomLineSegPt;

        // Calculate needed distances
        float horizDistSqrd = ptDisplacement.x * ptDisplacement.x + ptDisplacement.z * ptDisplacement.z;


        // Radius squared
        float radiusSqrd = capsuleRadius * capsuleRadius;


        // The answer will be undefined if the pt is horizontally outside of the capsule
        if(horizDistSqrd > radiusSqrd)
        {
            return pointToProject;
        }

        // Calculate the projected point
        float heightFromSegmentPt = -Mathf.Sqrt(radiusSqrd - horizDistSqrd);

        Vector3 projectedPoint = pointToProject;
        projectedPoint.y = bottomLineSegPt.y + heightFromSegmentPt;

        return projectedPoint;




    }

    public static float CalculateVerticalAngle(Vector3 direction)
    {
        //                 /|
        //                / |
        //               /  |
        //            h /   |
        //             /    | o
        //            /     |
        //           /ang   |
        //          /_______|
        //
        //sin(ang) = o/h, but since the dir is a unit vector h = 1
        //The angle will be: Asin(o)

        return Mathf.Rad2Deg * Mathf.Asin(direction.y);
    }


    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static Vector3 LerpTo(float easeSpeed, Vector3 start, Vector3 end, float dt)
    {
        Vector3 diff = end - start;

        diff *= Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return diff + start;
    }



    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static float LerpTo(float easeSpeed, float start, float end, float dt)
    {
        float diff = end - start;

        diff *= Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return diff + start;
    }

    public static Vector3 SlerpTo(float easeSpeed, Vector3 start, Vector3 end, float dt)
    {
        float percent = Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return Vector3.Slerp(start, end, percent);
    }

    //Clamps a vector along the x-z plane
    public static Vector3 HorizontalClamp(Vector3 v, float maxLength)
    {
        float horizLengthSqrd = v.x * v.x + v.z * v.z;

        if (horizLengthSqrd <= maxLength * maxLength)
        {
            return v;
        }

        float horizLength = Mathf.Sqrt(horizLengthSqrd);

        v.x *= maxLength / horizLength;
        v.z *= maxLength / horizLength;

        return v;
    }
}
