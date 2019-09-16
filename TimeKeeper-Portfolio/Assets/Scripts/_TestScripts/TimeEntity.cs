using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEntity : MonoBehaviour
{
    public UnityEvent TimeEvent;

    private void Start()
    {
        TimeWorld.Instance.RegisterEntity(this);
    }
}
