using App.Scripts.DungeonScene.Enemy;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Enemy enemy;
    [SerializeField] private SpriteRenderer enemyGFX;
    
    private float _health;

    private void Awake()
    {
        enemy.OnKnockBackEffect += KnockBackFeedBack;
    }

    private void KnockBackFeedBack()
    {
        enemyGFX.material = blinkMaterial;
        Invoke(nameof(ResetMaterial), 0.15f);
    }
    
    private void ResetMaterial()
    {
        enemyGFX.material = defaultMaterial;
    }
}
