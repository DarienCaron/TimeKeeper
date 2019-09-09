using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public Transform MuzzleLocation;
    public GameObject BulletPrefab;
    public GameObject Parent;

    public float FireDistance = 55.0f;
    

    void Start()
    {
        Parent = GetComponentInParent<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
         
        }

    }
}
