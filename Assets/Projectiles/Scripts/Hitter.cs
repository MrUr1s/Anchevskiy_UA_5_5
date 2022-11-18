using System;

public class Hitter:Receiver<ProjectileData>
{
    public event Action OnHit;
    private int _Layer;
    internal void SetLayer(int layer)
    {
        _Layer=layer;
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.TryGetComponent(out Healther healther) && healther.gameObject.layer != _Layer)
            healther.Damage(_data.Damage);

        OnHit?.Invoke();
        gameObject.SetActive(false);
        
    }
}