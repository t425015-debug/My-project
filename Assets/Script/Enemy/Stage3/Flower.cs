using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField, Header("‘¬“x")]
    private float _speed;
    [SerializeField, Header("SlowBullet")]
    private GameObject _bullet;
    [SerializeField, Header("î‚ÌŠp“x")]
    private float _ougiAngle;
    [SerializeField, Header("ËŒ‚ŠÔ")]
    private float _shootTime;
    [SerializeField, Header("ËŒ‚I—¹ŠÔ")]
    private float _finishTime;
    [SerializeField, Header("î’e‚Ì’e”")]
    private int _ougiBulletNum;



    private float _time1;
    private float _time2;
    private Rigidbody2D _rigid;
    Vector3 _startPos;
    private bool _isDown;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
         _startPos = transform.position;
        _time1 = 0;
        _time2 = 0;
    }

    void Update()
    {
        if( _isDown)
        {
            Vector2 dir = (_startPos - transform.position).normalized;
            _rigid.linearVelocity = dir * _speed;

            if(Vector2.Distance(transform.position, _startPos) < 0.1f)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (Vector2.Distance( transform.position, _startPos) <= 2f)
        {
            _rigid.linearVelocity = transform.up * _speed;
        }
        else
        {
            _rigid.linearVelocity = Vector2.zero;
            _Shoot();
        }

    }

    private void _Shoot()
    {
        _time1 += Time.deltaTime;
        _time2 += Time.deltaTime;
        if (_time2 > _finishTime)
        {
            _Down();
            return;
        }
        if (_time1 < _shootTime) return;

        for (int i = 0; i < _ougiBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * _ougiAngle;

            float theta =
                angleRange / (_ougiBulletNum - 1) * i
                - angleRange / 2f;

            GameObject bullet = Instantiate(_bullet);

            bullet.transform.position = transform.position;

            Vector3 dir =
                transform.up * Mathf.Cos(theta)
                + transform.right * Mathf.Sin(theta);

            bullet.transform.rotation =
                Quaternion.FromToRotation(Vector3.up, dir);
        }

        _time1 = 0;
    }

    
    private void _Down()
    {
        _isDown = true;
    }
}
