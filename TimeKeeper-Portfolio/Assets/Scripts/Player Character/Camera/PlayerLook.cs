using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float RotationSpeed;
    public float RotationSmoothing;

    public float MinPitch;
    public float MaxPitch;

    public string XAxisName;
    public string YAxisName;

    public Transform PlayerBody;

    private void Awake()
    {

        XAxisClamp = 0;
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        // Gets the horizontal rotation and stores it in Public variable.
        UpdateBodyRotation();

        // Updates the vertical rotation of the camera only.
        UpdateCameraRotation();

    }

    void UpdateBodyRotation()
    {
        Vector3 bodyRotation = new Vector3(0, Input.GetAxisRaw(XAxisName), 0);

        bodyRotation = bodyRotation * (RotationSpeed / RotationSmoothing);

        BodyRotation = bodyRotation;
        
    }

    void UpdateCameraRotation()
    {

        // Use Mouse Y to get our pitch
        Vector3 camRotation = new Vector3(Input.GetAxisRaw(YAxisName), 0, 0);
        camRotation = camRotation * (RotationSpeed / RotationSmoothing); // Multiply it by our speed and smoothing


        XAxisClamp += camRotation.x; // Increase our clamp rotation.

        if(CheckAxisPitch(ref XAxisClamp, MinPitch, MaxPitch)) // If its true, it means we don't want to rotate anymore. We've reached a constraint.
        {
            camRotation.x = 0;
        }

        CameraRotation = camRotation; // Store our current camera rotation;


        transform.Rotate(-CameraRotation ); // Rotate the camera only.

            
    }

    // Function to compare our axis constraint
    private bool CheckAxisPitch(ref float value, float min, float max )
    {
        if(value < min) // If the value is outside of the min or max it will return true.
        {
            value = min;
            return true;
        }
        else if(value > max)
        {
            value = max;
            return true;
        }
        else
        {
            return false;
        }

    }

   

    
    public Vector3 BodyRotation { get; private set; }
    public Vector3 CameraRotation { get; private set; }


    private float XAxisClamp;

}
