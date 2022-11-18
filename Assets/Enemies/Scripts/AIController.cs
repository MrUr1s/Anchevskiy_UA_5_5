using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Receiver<EnemyData>
{
    NavMeshAgent _navMeshAgent;
    protected override void OnReceive()
    {
        base.OnReceive();
        _navMeshAgent=GetComponent<NavMeshAgent>();
    }
    public void StartFollowing()
    {
        StartCoroutine(Following());
    }
    public void StopFollowing()
    {
        StopAllCoroutines();
    }

    private IEnumerator Following()
    {
        while(true)
        {
            _navMeshAgent.SetDestination(_data.Target.transform.position);
            yield return null; 
        }
    }
}
