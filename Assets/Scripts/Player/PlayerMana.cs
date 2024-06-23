using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : Singleton<PlayerMana>
{
    [SerializeField] private int maxMana = 100;
    private Slider manaSlider;
    public float currentMana;
    

    const string MANA_SLIDER_TEXT = "Mana Slider";

       private void Start() {
        currentMana = maxMana;
        UpdateManaSlider();
    }

    public void IncreaseMana(int manaAmount) {
        currentMana += manaAmount;
        if(currentMana >=maxMana){
            currentMana = maxMana;
        }
        UpdateManaSlider();
        Debug.Log("Mana increased: " + currentMana);
    }
    public void UseMana(float manaUsage){
        currentMana -= manaUsage;
        UpdateManaSlider();
    }

    private void UpdateManaSlider() {
        if (manaSlider == null) {
            manaSlider = GameObject.Find("Mana Slider").GetComponent<Slider>();
        }

        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }
}
