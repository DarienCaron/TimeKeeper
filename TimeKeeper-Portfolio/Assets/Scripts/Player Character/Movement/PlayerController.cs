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


        transform.position += directionInput * Time.deltaTime * MovementSpeed;
        currentDirection = directionInput;




    }

    private Vector3 currentDirection;
    private Vector3 currentVelocity;
}
