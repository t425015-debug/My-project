using UnityEngine;

public class OctpasEnemy : Enemy
{
    protected override void _Attack()
    {
        _Shooting();
    }

    private void _Shooting()
    {
        if (!_bAttack) return;

        _shootCount += Time.deltaTime;

        if (_shootCount < _shootTime) return;

        _CreateBullet(Vector3.up);
        _CreateBullet(Vector3.down);
        _CreateBullet(Vector3.left);
        _CreateBullet(Vector3.right);

        _shootCount = 0.0f;
    }

    private void _CreateBullet(Vector3 dir)
    {
        GameObject bulletObj = Instantiate(_bullet[0]);

        bulletObj.transform.position = transform.position;

        // ’e‚ÌŒü‚«‚ð•ÏX
        bulletObj.transform.up = dir;
    }
}