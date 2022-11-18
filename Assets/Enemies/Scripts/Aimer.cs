using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : Receiver<EnemyData>
{
    ShootingPoint _shootingPoint;

    protected override void OnReceive()
    {
        base.OnReceive();
        _shootingPoint = GetComponentInChildren<ShootingPoint>();
    }

    public void StartAiming()
    {
        StartCoroutine(Aiming());
    }
    public void StopAiming()
    {
        StopAllCoroutines();
    }
    IEnumerator Aiming()
    {
        while (true)
        {
            transform.LookAt(new Vector3(_data.Target.transform.position.x, transform.rotation.y, _data.Target.transform.position.z));
            _shootingPoint.transform.LookAt(_data.Target.transform.position);
            yield return null;
        }
    }
}
