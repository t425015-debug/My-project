using UnityEngine;

public class DashSideEnemy : Enemy
{
    protected override void _Initialize()
    {
        _moveVec = new Vector2(1f, 0);
        Vector3 dir = _moveVec;
    }
}
