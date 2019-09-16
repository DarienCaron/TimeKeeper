using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWorld : MonoBehaviour
{
    public static TimeWorld Instance;
    public static float TimeScale = 1;

    private List<TimeEntity> m_TimeEntitys;

    private void Awake()
    {
        m_TimeEntitys = new List<TimeEntity>();
        if(Instance == null)
        {
            Instance = this;
        }
        m_Time = 0;
    }

    public void RegisterEntity(TimeEntity t)
    {
        m_TimeEntitys.Add(t);
    }


    private void Update()
    {
        m_Time += Time.deltaTime;

        if (m_Time * TimeScale > 0.02f)
        {
            m_Time = 0;
            foreach(TimeEntity t in m_TimeEntitys)
            {
                t.TimeEvent.Invoke();
            }
        }
    }


    float m_Time;
}
