using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbarSystem : MonoBehaviour
{
    public Slider healthBar;

    public void UpdateHealthBar(float value)
    {
        healthBar.value = value;
    }
}
