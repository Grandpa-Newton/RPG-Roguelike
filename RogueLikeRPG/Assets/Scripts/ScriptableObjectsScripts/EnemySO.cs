using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemies")]
public class EnemySO : ScriptableObject
{
    public string name;
    public float health;
    public float speed;
    public int damage;
    public Sprite enemySprite;
    public AudioClip deathSound;
    public AudioClip walkSound;
}