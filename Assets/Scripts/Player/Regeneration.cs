using System.Collections;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField] private int healthRegenAmount = 1;
    [SerializeField] private int manaRegenAmount = 2;
    [SerializeField] private float regenInterval = 10f;

    private void Start()
    {
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);

            if (PlayerHealth.Instance != null)
            {
                if(!PlayerHealth.Instance.isPoisoned){
                    PlayerHealth.Instance.HealPlayer(healthRegenAmount);
                }
                 
            }

            if (PlayerMana.Instance != null)
            {
                PlayerMana.Instance.IncreaseMana(manaRegenAmount);
            }
        }
    }
}
