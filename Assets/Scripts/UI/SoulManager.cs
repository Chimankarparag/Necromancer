using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulManager : Singleton<SoulManager>
{
    private TMP_Text SoulText;
    private int currentSoul = 0;

    const string SOUL_AMOUNT_TEXT = "Souls Collected Text";

    public void UpdateCurrentSoul() {
        currentSoul += 1;

        if (SoulText == null) {
            SoulText = GameObject.Find(SOUL_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        SoulText.text = currentSoul.ToString("D3");
    }
}
