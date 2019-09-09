using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public Transform MuzzleLocation;
    public GameObject BulletPrefab;
    public GameObject Parent;
    

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
        Quaternion rot = Quaternion.LookRotation(Parent.transform.forward);

        if (Parent)
        {
            GameObject bullet = Instantiate(BulletPrefab, MuzzleLocation.position, rot);
         
        }

    }
}
