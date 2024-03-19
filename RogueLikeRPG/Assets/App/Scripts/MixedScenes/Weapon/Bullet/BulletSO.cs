using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_", menuName = "BulletSO")]
public class BulletSO : ScriptableObject
{
    public float lifeTime;
    public float distance;
    public Sprite departingBulletSprite;
    public TrailRenderer trailRenderer;
    //public ParticleSystem bulletTrailParticle;
}
