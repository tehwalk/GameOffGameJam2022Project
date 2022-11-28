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
    GameObject myGFX;
    [SerializeField] private float animTime = 10;
    [SerializeField] private GameObject hurtSFXPrefab, healedSFXPrefab, deadSFXPrefab;
    TextMeshProUGUI unitNameText, unitHealthText;
    Slider unitHealthSlider;

    private void Start()
    {
        unitHealthText = myHUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        unitNameText = myHUD.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        unitHealthSlider = myHUD.GetComponentInChildren<Slider>();
        myGFX = transform.GetChild(0).gameObject;
        unitNameText.text = unitName;
        unitHealthSlider.value = unitHealthSlider.maxValue;
        Health = maxHealth;
        unitHealthText.text = Health.ToString() + "/" + maxHealth.ToString();
    }
    public bool IsDead()
    {
        if (Health <= 0)
        {
            InstantiateSFX(deadSFXPrefab);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        unitHealthSlider.value = map(Health, 0, maxHealth, unitHealthSlider.minValue, unitHealthSlider.maxValue);
        unitHealthText.text = Health.ToString() + "/" + maxHealth.ToString();
        LeanTween.color(myGFX, Color.red, animTime * Time.deltaTime).setOnComplete(() =>
            {
                LeanTween.color(myGFX, Color.white, animTime * Time.deltaTime);
            }
            );
        InstantiateSFX(hurtSFXPrefab);
    }

    public void Heal(int healPoints)
    {
        if (Health < maxHealth) Health += healPoints;
        unitHealthSlider.value = map(Health, 0, maxHealth, unitHealthSlider.minValue, unitHealthSlider.maxValue);
        unitHealthText.text = Health.ToString() + "/" + maxHealth.ToString();
        LeanTween.color(myGFX, Color.cyan, animTime * Time.deltaTime).setOnComplete(() =>
           {
               LeanTween.color(myGFX, Color.white, animTime * Time.deltaTime);
           }
           );
        InstantiateSFX(healedSFXPrefab);
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return (b1 + (s - a1) * (b2 - b1) / (a2 - a1));
    }

    void InstantiateSFX(GameObject sfxPrefab)
    {
       GameObject sfx = Instantiate(sfxPrefab);
       AudioSource audio = sfx.GetComponent<AudioSource>();
       Destroy(sfx, audio.clip.length);
    }
}
