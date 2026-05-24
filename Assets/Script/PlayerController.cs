using Unity.VisualScripting;
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
    [SerializeField, Header("点滅時間")]
    private float _damageTime;
    [SerializeField, Header("点滅周期")]
    private float _damageCycle;

    private Vector2 _input;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private float _shootCount;
    private float _damageTimeCount;
    private bool _bDamage;


    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootCount = 0;
        _damageTimeCount = 0;
        _bDamage = false;
    }
    void Update()
    {
         _Move();
        _Damage();
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

        _shootCount += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            _Shooting();
        }

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
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            if (!_bDamage)
            {
                _hp -= 1;
                _bDamage = true;
                if (_hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void _Damage()
    {
        if (!_bDamage) return;

        _damageTimeCount += Time.deltaTime;

        float value = Mathf.Repeat(_damageTimeCount, _damageCycle);
        _spriteRenderer.enabled = value >= _damageCycle * 0.5f;
        
        if(_damageTimeCount >= _damageTime)
        {
            _damageTimeCount = 0;
            _spriteRenderer.enabled = true;
            _bDamage = false;
        } 
    }

    public void OnMOve(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

    }

    public int GetHP()
    {
        return _hp;
    }

    public bool IsDamage()
    {
        return _bDamage;
    }
}
