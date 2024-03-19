using UnityEngine;

namespace App.Scripts.DungeonScene.Enemy
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "Enemies")]
    public class EnemySO : ScriptableObject
    {
        public string enemyName;
        public float health;
        public float speed;
        public int damage;
        public Sprite enemySprite;
        public AudioClip deathSound;
        public AudioClip walkSound;
    }
}