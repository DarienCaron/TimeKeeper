using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public List<Weapon> ItemsToUse = new List<Weapon>();

    public GameObject PlayerArm;


    private void Awake()
    {
        m_CurrentItemIndex = 0;
        m_OriginalHandPos = PlayerArm.transform.localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {


        EquippedItem = ItemsToUse[m_CurrentItemIndex];
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EquippedItem.Use();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(EquippedItem.EquipmentType == EquipmentType.Gun)
            {
                Gun g = (Gun)EquippedItem;
                g.Reload();
            }
        }

        if (EquippedItem.EquipmentType == EquipmentType.Gun)
        {
            Gun g = (Gun)EquippedItem;
            if (Input.GetKey(KeyCode.Mouse1))
            {

                PlayerArm.transform.localPosition = Vector3.Slerp(PlayerArm.transform.localPosition, g.ADSLocation, Time.deltaTime * g.ADSSpeed);


            }

            else
            {
                if ((PlayerArm.transform.localPosition - m_OriginalHandPos).magnitude > 0.1f)
                {
                    PlayerArm.transform.localPosition = Vector3.Slerp(PlayerArm.transform.localPosition, m_OriginalHandPos, Time.deltaTime *  g.ADSSpeed);
                }
            }
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

    void ChangeEquipment(int itemIndex)
    {
        if (itemIndex <= ItemsToUse.Count)
        {
            EquippedItem = ItemsToUse[itemIndex];
        }
    }


    public Equipment EquippedItem { get; private set; }

    private int m_CurrentItemIndex;
    private Vector3 m_OriginalHandPos;
}
