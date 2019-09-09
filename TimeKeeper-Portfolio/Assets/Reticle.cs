using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{

    RectTransform UITransform;
    // Start is called before the first frame update
    void Start()
    {
        UITransform = GetComponent<RectTransform>();

        float x = Screen.width / 2;
        float y = Screen.height / 2;

        Ray b = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        Vector3 loc = Camera.main.WorldToScreenPoint(b.origin);



     
        UITransform.position = loc;
    }

    private void Update()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;

        Ray b = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        Vector3 loc = Camera.main.WorldToScreenPoint(b.origin);




        UITransform.position = loc;
    }




}
