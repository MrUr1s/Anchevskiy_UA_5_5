using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : APool<ProjectileData>
{
    public ProjectilePool(ProjectileData prefab, Transform parent,int count =1) : base(prefab, parent)
    {
        Init(count);
    }

    protected override ProjectileData GetCreated()
    {
      return Object.Instantiate(_prefab);
    }

}
