using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("弾オブジェクト")]
    private GameObject _bullet;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;
    [SerializeField, Header("体力")]
    private int _hp;

    Vector2 _input;
    private Rigidbody2D _rigid;
    private float _shootCount;

    private void Start()
    {
     _rigid = GetComponent<Rigidbody2D>();
        _shootCount = 0;
    }
    void Update()
    {
         _Move();
            _shootCount += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            _Shooting();
        }
    }
    private void _Move()
    {
        //キーボードの入力方向に動く
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        transform.Translate(_input.normalized * moveSpeed * Time.deltaTime);

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -8f, 8f);
        pos.y = Mathf.Clamp(pos.y, -4f, 4f);

        transform.position = pos;
    }

    private void _Shooting()
    {
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bullet);
        bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);
        _shootCount = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            _hp -= 1;
            if(_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public int GetHP()
    {
        return _hp;
    }
}
