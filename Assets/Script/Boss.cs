using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UIElements;

public class Boss : Enemy
{
    [SerializeField, Header("ˆÚ“®”ÍˆÍ")]
    private float _limitPosY;
    [SerializeField, Header("’ÊíUŒ‚")]
    private int _normalAttackCount;

    enum AttackMode
    {
        Normal,
        Ougi,
    }

    private int _currentNormalAttackCount;
    private AttackMode _attackMode;

    protected override void _Initialize()
    {
        _currentNormalAttackCount = 0;
        _attackMode = AttackMode.Normal;
    }

    protected override void _Move()
    {
        if (transform.position.y <= _limitPosY)
        {
            _rigid.linearVelocity = Vector2.zero;
            _bAttack = true;
            return;
        }
        base._Move();
        _bAttack = false;
    }

    protected override void _Attack()
    {
        switch (_attackMode)
        {
            case AttackMode.Normal: _NormalShooting(); break;
            case AttackMode.Ougi: break;
        }
    }

    private void _NormalShooting()
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bullet[0]);
        bulletObj.transform.position = transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, Vector2.down);

        _shootCount = 0f;
        _currentNormalAttackCount++;

        if(_currentNormalAttackCount >= _normalAttackCount)
        {
            _attackMode = AttackMode.Ougi;
            _currentNormalAttackCount = 0;
        }
    }
}
