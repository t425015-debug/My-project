using System.Collections;
using UnityEngine;

public class SlowBullet : Bullet
{
    [SerializeField, Header("スピード減少量")]
    public float _cutSpeed;
    [SerializeField, Header("減少時間")]
    public float _decaySpeed;

    private float _currentTime;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        collision.gameObject.GetComponent<PlayerController>()._Slow(_cutSpeed, _decaySpeed);

    }
}
