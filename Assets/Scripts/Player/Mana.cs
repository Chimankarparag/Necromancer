using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : Singleton<Mana>
{
    [SerializeField] private int maxMana = 100;
    private Slider manaSlider;
        private float currentMana;

    const string MANA_SLIDER_TEXT = "Mana Slider";

       private void Start() {
        currentMana = 0;
        UpdateManaSlider();
    }

        public void IncreaseMana() {
        currentMana += 10;
        UpdateManaSlider();
        Debug.Log("Mana increased: " + currentMana);
    }

        private void UpdateManaSlider() {
        if (manaSlider == null) {
            manaSlider = GameObject.Find("Mana Slider").GetComponent<Slider>();
        }

        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }
}
