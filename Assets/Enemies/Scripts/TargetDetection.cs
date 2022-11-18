using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : Receiver<EnemyData>
{
    public event Action OnDetect;
    public event Action OnLost;

    bool _isDetection = true;

    private void Update()
    {
        Detect();
    }

    private void Detect()
    {
       if(_data.Target!=null)
        {
            if(IsDetected()&&_isDetection)
            {
                OnDetect?.Invoke();
                _isDetection = false;
            }
            if(!IsDetected()&&!_isDetection)
            {
                OnLost?.Invoke();
                _isDetection=true;
            }
        }
    }

    private bool IsDetected()
    {
        return Vector3.Distance(transform.position, _data.Target.transform.position) < _data.DetectionDistance;
    }
}
