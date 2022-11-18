using UnityEngine;
public class Receiver<T>:MonoBehaviour
{
    protected T _data;

    private void Awake()
    {
        OnReceive();
    }
    protected virtual void OnReceive() => _data = GetComponent<T>();
}