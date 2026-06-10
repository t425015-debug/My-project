using UnityEngine;

public class DashEnemy : Enemy
{
    protected override void _Initialize()
    {
        _moveVec = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 0f));
        Vector3 dir = (transform.position + (Vector3)(-_moveVec)) - transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, dir);
    }
}
