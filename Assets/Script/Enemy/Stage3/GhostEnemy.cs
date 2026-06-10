using UnityEngine;

public class GhostEnemy : Enemy
{
    [SerializeField]
    private float _leftRightMoveSpeed;

    private int _dir2 = 1;

    protected override void _Attack()
    {
        _Shooting();
        _Move(new Vector2(1f, 0f));
        _Move(new Vector2(-1f, 0f));
    }

    private void _Shooting()
    {
        if (!_bAttack) return;

        _shootCount += Time.deltaTime;

        if (_shootCount < _shootTime) return;

        _CreateBullet(_player.gameObject.transform.position);
        _shootCount = 0.0f;
    }

    private void _CreateBullet(Vector3 dir)
    {
        GameObject bulletObj = Instantiate(_bullet[0]);

        bulletObj.transform.position = transform.position;

        // ’e‚ÌŒü‚«‚ð•ÏX
        bulletObj.transform.up = dir - bulletObj.transform.position;
    }

    private void _Move(Vector2 _vec)
    {
        transform.position +=
            Vector3.right * _dir2 * _leftRightMoveSpeed * Time.deltaTime;

        // ¶‰E”½“]
        if (transform.position.x > 5f)
        {
            _dir2 = -1;
        }
        else if (transform.position.x < -5f)
        {
            _dir2 = 1;
        }
    }

}
