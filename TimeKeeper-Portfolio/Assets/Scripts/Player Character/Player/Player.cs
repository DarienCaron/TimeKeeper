using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public List<Weapon> ItemsToUse = new List<Weapon>();

    public GameObject PlayerArm;
   


    private void Awake()
    {
        m_CurrentItemIndex = 0;
        GunController = GetComponent<GunController>();
      
    }
    // Start is called before the first frame update
    void Start()
    {


       
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GunController.OnTriggerHold();
        }


        if(Input.GetKeyDown(KeyCode.R))
        {
            GunController.OnReload();
        }
   
        if(Input.GetMouseButtonDown(0))
        {
            GunController.ChangeAim(AimState.Aiming);
        }
        if (Input.GetMouseButtonUp(0))
        {
            GunController.ChangeAim(AimState.Hipfire);
        }


        // TEMP CODE
        if (Input.GetKey(KeyCode.J))
        {
            TimeWorld.Instance.HalfTime();
        }
        else
        {
            TimeWorld.Instance.FullTime();
        }


      

        //if(ItemsToUse.Count > 1)
        //{
        //    float d = Input.GetAxis("Mouse ScrollWheel");
        //    if (d > 0)
        //    {
        //        m_CurrentItemIndex++;

        //    }
        //    else if(d < 0)
        //    {
        //        m_CurrentItemIndex--;
        //    }

        //    if(m_CurrentItemIndex < 0)
        //    {
        //        m_CurrentItemIndex = 0;
        //    }

        //    ChangeEquipment(m_CurrentItemIndex);
        //}
    }

  


    public GunController GunController { get; private set; }

    private int m_CurrentItemIndex;
    private Vector3 m_OriginalHandPos;
}
