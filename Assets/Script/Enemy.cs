using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;
    [SerializeField, Header("弾を発射する時間")]
    protected float _shootTime;
    [SerializeField, Header("体力")]
    private int _hp;
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    protected GameObject _player;
    protected Rigidbody2D _rigid;
    protected float _shootCount;
    protected bool _bAttack;

    void Start()
    {
        if(FindFirstObjectByType<PlayerController>())
        {
            _player = FindFirstObjectByType<PlayerController>().gameObject;
        }
        _shootCount = 0;
        _bAttack = false;
        _rigid = GetComponent<Rigidbody2D>();
        _Initialize();
    }

    protected virtual void _Initialize()
    {

    }

    void Update()
    {
        _Move();
        _Attack();
    }

    protected virtual void _Attack()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _hp -= 1;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void _Move()
    {
       _rigid.linearVelocity = Vector2.down * _moveSpeed;
    }

    private void OnBecameVisible()
    {
            _bAttack = true;
    }
    private void OnBecameInvisible()
    {
        if (_bAttack)
        {
            Destroy(gameObject);
        }
    }


}
