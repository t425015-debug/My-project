using UnityEngine;

public class DashSideEnemy : Enemy
{
    [SerializeField, Header("Œü‚«")]
    private int vectol;

    protected override void _Initialize()
    {
        _moveVec = new Vector2(vectol, 0);
        Vector3 dir = _moveVec;
    }
}
