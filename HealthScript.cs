using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    public float health = 100;
   
    public float mana = 100;
    public float experience=0;
    public Slider healthSlider;
    public Slider manaSlider;
    
    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
      
        
        healthSlider.maxValue = health;
        manaSlider.maxValue = mana;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int minDamage,int maxDamage)
    {
        
        health -= Random.Range(minDamage,maxDamage);
        healthSlider.value = health;
    }
    public void TakeMana(int manaValue)
    {
        mana -= manaValue;
        manaSlider.value = mana;
    }
   public void Heal(int healMinValue,int healMaxValue)
    {
        health += Random.Range(healMinValue,healMaxValue);
        healthSlider.value = health;
    }
}
