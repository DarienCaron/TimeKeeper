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
        float x = Screen.width / 2;
        float y = Screen.height / 2;

        Ray b = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));


        Quaternion rot = Quaternion.LookRotation(b.direction);

     


        if (Parent)
        {
            GameObject bullet = Instantiate(BulletPrefab, MuzzleLocation.position, rot);
         
        }

    }
}
