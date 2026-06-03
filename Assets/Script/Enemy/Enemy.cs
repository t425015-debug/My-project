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
    [SerializeField, Header("経験値")]
    public int _exp;
    [SerializeField, Header("ダメージエフェクトの時間")]
    private float _damageEffectTime;
    [SerializeField, Header("ダメージ時の画像")]
    private Sprite _damageSprite;
    [SerializeField, Header("死亡エフェクト")]
    private GameObject _deadEffect;

    protected GameObject _player;
    protected Rigidbody2D _rigid;
    protected Vector2 _moveVec;
    protected float _shootCount;
    protected bool _bAttack;

    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    protected GameManager _gameManager;

    void Start()
    {
        if(FindFirstObjectByType<PlayerController>())
        {
            _player = FindFirstObjectByType<PlayerController>().gameObject;
        }
        _shootCount = 0;
        _bAttack = false;
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _moveVec = Vector2.down;
        _gameManager = FindAnyObjectByType<GameManager>();
        _Initialize();
    }

    protected virtual void _Initialize()
    {

    }

    void Update()
    {
        if (_gameManager.IsShowResult()) return;
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
            PlayerController player = _player.GetComponent<PlayerController>();
            _hp -= collision.gameObject.GetComponent<Bullet>().GetPower() + player.GetPower();
            StartCoroutine(_Damage());
            if (_hp <= 0)
            {
                _Dead();
            }
        }
    }

    protected virtual void _Move()
    {
       _rigid.linearVelocity = _moveVec * _moveSpeed;
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

    private IEnumerator _Damage()
    {
        _spriteRenderer.sprite = _damageSprite;
        yield return new WaitForSeconds(_damageEffectTime);
        _spriteRenderer.sprite = _defaultSprite;
    }

    protected virtual void _Dead()
    {
        if (_player != null)
        {
            _player
                .GetComponent<PlayerController>()
                .AddExp(_exp);
        }
        StartCoroutine(_DeadEffect());
        Destroy(gameObject);
    }

    IEnumerator _DeadEffect()
    {
        GameObject effect = Instantiate(_deadEffect, transform.position, Quaternion.identity);
        effect.transform.localScale = transform.localScale;
        yield return new WaitForSeconds(2f); 
    }
}
