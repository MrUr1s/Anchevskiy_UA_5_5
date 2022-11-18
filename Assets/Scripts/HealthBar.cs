using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }
    public void Redraw(int maxHealth, int health)
    {
        _slider.value = health>0? (float) health/maxHealth:0;
    }
}
