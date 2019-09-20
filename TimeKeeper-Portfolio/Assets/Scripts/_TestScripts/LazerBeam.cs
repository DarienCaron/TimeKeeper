using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LazerBeam : MonoBehaviour
{
    public Color Color = Color.green;
   


    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        if (m_GunParent == null)
        {
            m_GunParent = GetComponentInParent<Gun>();
        }

        m_LineRenderer.material.color = Color;

        UpdateLazer();
    }

    private void Update()
    {
        UpdateLazer();
    }
    
    private void UpdateLazer()
    {
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, m_GunParent.MuzzleLocation.position);
    }

    private LineRenderer m_LineRenderer;
    private Gun m_GunParent;
}
