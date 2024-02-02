using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemies")]
public class EnemySO : ScriptableObject
{
    public string name;
    public int health;
    public float speed;
    public int damage;
    public Sprite enemySprite;
    public AudioClip deathSound;
    public AudioClip walkSound;
}