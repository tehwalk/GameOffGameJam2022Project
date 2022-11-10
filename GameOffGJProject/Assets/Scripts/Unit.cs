using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public string unitName;
    private int Health;
    public int maxHealth;
    public GameObject myHUD;
    TextMeshProUGUI unitNameText;
    Slider unitHealthSlider;

    private void Start()
    {
        unitNameText = myHUD.GetComponentInChildren<TextMeshProUGUI>();
        unitHealthSlider = myHUD.GetComponentInChildren<Slider>();
        unitNameText.text = unitName;
        unitHealthSlider.value = unitHealthSlider.maxValue;
        Health = maxHealth;
    }
    public bool IsDead(int dmg)
    {
        Health -= dmg;
        unitHealthSlider.value = map(Health, 0, maxHealth, unitHealthSlider.minValue, unitHealthSlider.maxValue);
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return (b1 + (s - a1) * (b2 - b1) / (a2 - a1));
    }
}
