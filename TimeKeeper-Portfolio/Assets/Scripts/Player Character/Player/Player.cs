using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public List<Weapon> ItemsToUse = new List<Weapon>();

    private void Awake()
    {
        m_CurrentItemIndex = 0;
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


    public IEquipment EquippedItem { get; private set; }

    private int m_CurrentItemIndex;
}
