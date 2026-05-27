using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("弾オブジェクト")]
    private GameObject _bullet;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;
    [SerializeField, Header("残弾数リスト")]
    private List<int> _remainBulletList;
    [SerializeField]
    private TextMeshProUGUI _remainBulletText;
    [SerializeField, Header("レベル")]
    private int _level;
    [SerializeField, Header("経験値リスト")]
    private List<int> _expList;
    [SerializeField]
    private TextMeshProUGUI _revelText;
    [SerializeField, Header("体力")]
    private int _hp;
    [SerializeField, Header("点滅時間")]
    private float _damageTime;
    [SerializeField, Header("点滅周期")]
    private float _damageCycle;
    [SerializeField, Header("死亡エフェクト")]
    private GameObject _deadEffect;

    private Vector2 _input;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;
    private CinemachineImpulseSource _shaker;
    private float _shootCount;
    private float _damageTimeCount;
    private bool _bDamage;
    private Enemy _enemy;
    private int _expCount;
    private int _remainBullet;


    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindFirstObjectByType<GameManager>();
        _shaker = FindAnyObjectByType<CinemachineImpulseSource>();
        _enemy = FindFirstObjectByType<Enemy>();
        _shootCount = 0;
        _damageTimeCount = 0;
        _bDamage = false;    
        _expCount = 0;
        _remainBullet = _remainBulletList[_level - 1];
    }
    void Update()
    {
         _Move();
        _Damage();
        _revelText.text = $"Lv:{_level}";
        _remainBulletText.text = $"Bullet{_remainBullet}";
        _LevelCheck();
    }
    private void _Move()
    {
        //キーボードの入力方向に動く
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        transform.Translate(_input.normalized * moveSpeed * Time.deltaTime);

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -8f, 8f);
        pos.y = Mathf.Clamp(pos.y, -4f, 4f);

        transform.position = pos;

        _shootCount += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            _Shooting();
        }

    }

    private void _Shooting()
    {
        if (_shootCount < _shootTime) return;
        if(_remainBullet == 0)
        {
            Debug.Log("残弾が0です");
            return;
        }

        GameObject bulletObj = Instantiate(_bullet);
        bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);
        _shootCount = 0.0f;
        _remainBullet--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _Hit(collision.gameObject);
    }

    private void _Damage()
    {
        if (!_bDamage) return;

        _damageTimeCount += Time.deltaTime;

        float value = Mathf.Repeat(_damageTimeCount, _damageCycle);
        _spriteRenderer.enabled = value >= _damageCycle * 0.5f;
        
        if(_damageTimeCount >= _damageTime)
        {
            _damageTimeCount = 0;
            _spriteRenderer.enabled = true;
            _bDamage = false;
        } 
    }

    private void _Hit(GameObject hitobj)
    {
        if (_bDamage) return;

        if(hitobj.tag == "Bullet")
        {
            _hp -= hitobj.GetComponent<Bullet>().GetPower();
        }
        else if(hitobj.tag == "Enemy")
        {
            _hp -= 1;
        }

        _bDamage = true;
        if(_hp <= 0)
        {
            Destroy(gameObject);
            Instantiate(_deadEffect, transform.position, Quaternion.identity);
            _gameManager.DeadEffect(GameManager.ResultMode.GameOver);
            _shaker.GenerateImpulse();

        }
    }

    public void OnMOve(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

    }

    public int GetHP()
    {
        return _hp;
    }

    public bool IsDamage()
    {
        return _bDamage;
    }

    private void _LevelCheck()
    {
        if (_level > _expList[_expList.Count - 1]) return; // 最大レベルならreturn

        if (_expList[_level - 1] - _expCount <= 0)
        {
            _expCount -= _expList[_level - 1];
            _level++;
            _remainBullet = _remainBulletList[_level - 1];
        }
    }

    public void AddExp(int exp)
    {
        _expCount += exp;

        Debug.Log("EXP : " + _expCount);

        _LevelCheck();
    }
}
