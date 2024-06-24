using System.Collections;
using UnityEngine;

public class EnemyObstacle : MonoBehaviour
{
    [SerializeField] public float obstacleDamage = 5f;
    [SerializeField] public bool Poisonous = true;
    [SerializeField] public int poisonDamage = 10;
    [SerializeField] public float poisonInterval = 2f;
    [SerializeField] public float poisonDuration = 10f;

    [SerializeField] private Sprite newSprite;
    private Flash flash;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        flash = GetComponent<Flash>();
    }

    private void OnCollisionStay2D(Collision2D eother)
    {
        PlayerHealth player = eother.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(ChangeSpriteTemporarily());
        }
    }

    private IEnumerator ChangeSpriteTemporarily()
    {
        spriteRenderer.sprite = newSprite;
        yield return new WaitForSeconds(5f);
        StartCoroutine(flash.FlashRoutine());
        spriteRenderer.sprite = originalSprite;
    }
}
