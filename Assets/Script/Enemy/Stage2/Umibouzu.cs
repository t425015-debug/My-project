using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Umibouzu : Enemy
{
    enum AttackMode
    {
        Normal,     // 通常弾
        WaterTower,
        SpornEnemy,
        Normal2,
    }

    [SerializeField, Header("BGM")]
    private AudioClip _bgm;

    [SerializeField, Header("")]
    private AnimationClip _bossIdleAnimation;


    [SerializeField]
    private FlagData _flagData;

    [SerializeField, Header("移動範囲")]
    private float _limitPosY;
    [SerializeField, Header("ダメージ範囲オブジェクト")]
    private GameObject _damageCircle;
    [SerializeField, Header("通常攻撃1")]
    private int _normalAttackCount1;
    [SerializeField, Header("通常攻撃2")]
    private int _normalAttackCount2;

    [SerializeField, Header("水のタワー")]
    private int _waterTowerAttackCount;
    [SerializeField]
    private float _waterTowerPosition;


    [SerializeField, Header("スポーンエネミー")]
    private int _spornEnemyAttackCount;

    private int _currentAttackCount;
    private AttackMode _attackMode;

    bool _isWaterTower;
    bool _isSpornEnemy;
    bool _isAttackAnimation;

    Animator _animator;

    protected override void _Initialize()
    {
         _animator = GetComponent<Animator>();
        StartCoroutine( _BGM());
        _currentAttackCount = 0;
        _attackMode = AttackMode.Normal;
        _isWaterTower = false;
        _moveVec = Vector3.up;
        _bAttack = true;
        _isSpornEnemy = false;
    }


    protected override void _Attack()
    {
        if (!_bAttack) return;
        switch (_attackMode)
        {
            case AttackMode.Normal: _NormalShoot(_normalAttackCount1, AttackMode.WaterTower); break;
            case AttackMode.WaterTower:
                if (!_isWaterTower)
                {
                    StartCoroutine(_WaterTower());
                }
                break;
            case AttackMode.SpornEnemy:
                if (!_isSpornEnemy)
                {
                    _isSpornEnemy = true;
                    StartCoroutine(_SpornEnemy());
                }
                break;
            case AttackMode.Normal2:
                _NormalShoot(_normalAttackCount2, AttackMode.WaterTower);
                break;

        }
    }

    protected override void _Dead()
    {
        _gameManager.DeadEffect(GameManager.ResultMode.GameClear);
        _flagData._stageCleer2 = true;
        base._Dead();
    }
    
    private void _NormalShoot(int _normalAttackCount, AttackMode attackMode)
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;
        if(_player == null) return;

        GameObject bulletObj = Instantiate(_bullet[0]);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - bulletObj.transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        bulletObj.transform.localScale = new Vector3( transform.localScale.x,-transform.localScale.y,transform.localScale.z);

        _shootCount = 0f;
        _currentAttackCount++;

        if (_currentAttackCount >= _normalAttackCount)
        {
            _attackMode = attackMode;
            _currentAttackCount = 0;
        }
    }

    IEnumerator _WaterTower()
    {
        _isWaterTower = true;
        yield return new WaitForSeconds(1);

        if (_player == null)
        {
            _isWaterTower = false;
            yield break;
        }
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) yield break;

        GameObject _damageArea = Instantiate(_damageCircle);
        _damageArea.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 1);
        yield return new WaitForSeconds(1f);

        GameObject _waterTower = Instantiate(_bullet[1]);
        _waterTower.transform.position = new Vector2(_damageArea.transform.position.x, _damageArea.transform.position.y + _waterTowerPosition);
        yield return new WaitForSeconds(0.5f);
        Destroy(_waterTower);
        Destroy(_damageArea);
        _isWaterTower = false;

        _shootCount = 0f;
        _currentAttackCount++;
        if (_currentAttackCount >= _waterTowerAttackCount)
        {
            _attackMode = AttackMode.SpornEnemy;
            _currentAttackCount = 0;
        }
    }

    IEnumerator _SpornEnemy()
    {
        yield return new WaitForSeconds(1);

        GameObject _damageArea1 = Instantiate(_damageCircle);
        _damageArea1.transform.position = new Vector3(3f, 1f, 0f);

        GameObject _damageArea2 = Instantiate(_damageCircle);
        _damageArea2.transform.position = new Vector3(-3f, 1f, 0f);

        yield return new WaitForSeconds(1f);

        GameObject _enemy1 = Instantiate(_bullet[2]);
        _enemy1.transform.position = _damageArea1.transform.position;

        GameObject _enemy2 = Instantiate(_bullet[2]);
        _enemy2.transform.position = _damageArea2.transform.position;

        yield return new WaitForSeconds(0.5f);

        Destroy(_damageArea1);
        Destroy(_damageArea2);

        _isSpornEnemy = false;

        _currentAttackCount++;

        if (_currentAttackCount >= _spornEnemyAttackCount)
        {
            _attackMode = AttackMode.Normal2;
            _currentAttackCount = 0;
        }

    }

    private IEnumerator _BGM()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayBGM(_bgm);
    }

}

