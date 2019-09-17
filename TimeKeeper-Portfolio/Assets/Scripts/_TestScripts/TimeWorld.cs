using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWorld : MonoBehaviour
{
    public static TimeWorld Instance;
   

    private List<TimeEntity> m_TimeEntitys;

    private void Awake()
    {
        m_TimeEntitys = new List<TimeEntity>();
        if(Instance == null)
        {
            Instance = this;
        }
        TimeScale = 1;
      
    }

    public void RegisterEntity(TimeEntity t)
    {
        m_TimeEntitys.Add(t);
    }

    public void StopTime()
    {
        TimeScale = 0;
    }

    public void HalfTime()
    {
        TimeScale = 0.5f;

    }

    public void FullTime()
    {
        TimeScale = 1;
    }


    public float TimeScale { get; private set; }

  


    
}
