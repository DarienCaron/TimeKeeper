using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float RotationSpeed = 3f;
    public float MovementSpeed = 5f;



    // TODO Implement basic player controllers such as rotating and moving. To be cleaned and optimized later

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 directionInput = new Vector3(x, 0, z);
        directionInput.Normalize();

        Vector3 rot = new Vector3(0,Input.GetAxis("Mouse X"), 0);// Rotation code for now. 
        rot.x = 0;
        
        transform.Rotate(rot * Time.deltaTime * RotationSpeed);//


        transform.position += directionInput * Time.deltaTime * MovementSpeed; // Need to account for current direction.
        currentDirection = directionInput;

      


    }

    private Vector3 currentDirection;
    private Vector3 currentVelocity;
}
