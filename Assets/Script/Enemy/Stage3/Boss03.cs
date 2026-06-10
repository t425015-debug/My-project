using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Boss03 : Enemy
{
    enum AttackMode
    {
        Normal,    
        SlowDown,
        Sporn,
        SlowSide,
    }

    [SerializeField, Header("BGM")]
    private AudioClip _bgm;

    [SerializeField]
    private FlagData _flagData;

    [SerializeField, Header("’ÊíUŒ‚")]
    private int _normalAttackCount;

    [SerializeField, Header("’ÊíUŒ‚‚©‚çŸ‚ÌUŒ‚‚ÖˆÚ‚éŠÔ")]
    private float _normalAttackNextTime;
    [SerializeField, Header("ƒXƒƒEƒ_ƒEƒ“UŒ‚‚©‚çŸ‚ÌUŒ‚‚Ö‰f‚éŠÔ")]
    private float _slowDownAttackNextTime;
    [SerializeField, Header("ƒXƒ|[ƒ“UŒ‚‚©‚çŸ‚ÌUŒ‚‚ÍˆÚ‚éŠÔ")]
    private float _spornAttackNextTime;
    [SerializeField, Header("ƒXƒƒEƒTƒCƒhUŒ‚‚©‚çŸ‚ÌUŒ‚‚ÉˆÚ‚éŠÔ")]
    private float _slowSideAttackNextTime;

    private int _currentAttackCount;
    private AttackMode _attackMode;
    private int _slowDownCount;
    private int _slowSideCount;
    private int _spornCount;
    private bool _isSporn;
    private bool _isSlowDown;
    private bool _isSlowSide;

    protected override void _Initialize()
    {
        StartCoroutine(_BGM());
        _currentAttackCount = 0;
        _attackMode = AttackMode.Normal;
        _moveVec = Vector3.up;
        _bAttack = true;
        _slowDownCount = 1;
        _slowSideCount = 1;
        _spornCount = 1;
    }

    private IEnumerator _BGM()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayBGM(_bgm);
    }

    protected override void _Dead()
    {
        _gameManager.DeadEffect(GameManager.ResultMode.GameClear);
        _flagData._stageCleer3 = true;
        base._Dead();
    }



    protected override void _Attack()
    {
        if (!_bAttack) return;
        switch (_attackMode)
        {
            case AttackMode.Normal: _NormalShooting(AttackMode.SlowDown); break;
            case AttackMode.SlowDown:
                    _SlowShootDown();
                break;
            case AttackMode.Sporn:
                    _Sporn();
                break;
            case AttackMode.SlowSide:
                    _SlowShootSide();
                break;
        }
    }

    private void _NormalShooting(AttackMode attackMode)
    {
       
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;
        if (_player == null) return;
        Vector2 dir = new Vector2(0, -1);
        GameObject bulletObj1 = Instantiate(_bullet[0]);
        GameObject bulletObj2 = Instantiate(_bullet[0]);
        GameObject bulletObj3 = Instantiate(_bullet[0]);
        bulletObj1.transform.position = new Vector3(transform.position.x + 5f, transform.position.y + 8f);
        bulletObj2.transform.position = new Vector3(transform.position.x, transform.position.y + 8f);
        bulletObj3.transform.position = new Vector3(transform.position.x + -5f, transform.position.y + 8f);
        bulletObj1.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        bulletObj2.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        bulletObj3.transform.rotation = Quaternion.FromToRotation(transform.up, dir);


        _shootCount = 0f;
        _currentAttackCount++;


        if (_currentAttackCount >= _normalAttackCount)
        {
            _currentAttackCount = 0;
            StartCoroutine(_WaitNormalAttack(attackMode));
        }
    }

    private void _SlowShootDown()
    {
        if (_isSlowDown) return;
            _isSlowDown = true;

            GameObject flower = Instantiate(_bullet[1]);
            flower.transform.position = new Vector3(0f, -8f, 0f);

            _currentAttackCount++;

        if (_currentAttackCount >= _slowDownCount)
        {
            _currentAttackCount = 0;
            StartCoroutine(_WaitSlowDownAttack());
        }
    }

    private void _Sporn()
    {
        if(_isSporn ) return;
        _isSporn = true;

        Instantiate(_bullet[2]);

        _currentAttackCount++;

        if (_currentAttackCount >= _spornCount)
        {
            _currentAttackCount = 0;
            StartCoroutine(_WaitSpornAttack());
        }

    }

    private void _SlowShootSide()
    {
        if( _isSlowSide ) return;
        _isSlowSide = true;

        GameObject _flower1 = Instantiate(_bullet[1]);
        GameObject _flower2 = Instantiate(_bullet[1]);
        _flower1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        _flower2.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        _flower1.transform.position = new Vector3(12f, 0f, 0f);
        _flower2.transform.position = new Vector3(-12f, 0f, 0f);

        _currentAttackCount++;

        if (_currentAttackCount >= _slowSideCount)
        {
            _currentAttackCount = 0;
            StartCoroutine(_WaitSlowSideAttack());
        }

    }

    private IEnumerator _WaitNormalAttack(AttackMode attack)
    {
        yield return new WaitForSeconds(_normalAttackNextTime);
        _attackMode = attack;
    }
    private IEnumerator _WaitSlowDownAttack()
    {
        yield return new WaitForSeconds(_slowDownAttackNextTime);
        _isSlowDown = false;
        _attackMode = AttackMode.Sporn;
    }
    private IEnumerator _WaitSpornAttack()
    {
        yield return new WaitForSeconds(_spornAttackNextTime);
        _isSporn = false;
        _attackMode = AttackMode.SlowSide;
    }
    private IEnumerator _WaitSlowSideAttack()
    {
        yield return new WaitForSeconds(_slowSideAttackNextTime);
        _isSlowSide = false;
        _attackMode = AttackMode.Normal;
    }

}
