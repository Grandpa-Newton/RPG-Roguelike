using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_", menuName = "Bullets")]
public class BulletSO : ScriptableObject
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public Sprite bulletSprite;
}
