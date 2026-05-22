using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    private GameObject _bullet;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;
    [SerializeField, Header("体力")]
    private int _hp;
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    private GameObject _player;
    private Rigidbody2D _rigid;
    private float _shootCount;
    private bool _bAttack;
    Vector3 dir;

    void Start()
    {
        if(FindFirstObjectByType<PlayerController>())
        {
            _player = FindFirstObjectByType<PlayerController>().gameObject;
        }
        _shootCount = 0;
        _bAttack = false;
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _Shooting();
        _Move();
    }

    private void _Shooting()
    {
        if (_player == null) return;
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bullet);
        bulletObj.transform.position = transform.position;
            dir = _player.transform.position - transform.position;
            bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _shootCount = 0.0f;
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

    private void _Move()
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
