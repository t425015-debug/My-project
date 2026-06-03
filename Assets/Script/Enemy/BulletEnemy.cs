using UnityEngine;

public class BulletEnemy : Enemy
{
    protected override void _Attack()
    {
        Debug.Log("a");
        _Shooting();
    }

    private void _Shooting()
    {
        if (_player == null || !_bAttack) return;
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bullet[0]);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _shootCount = 0.0f;
    }
}
