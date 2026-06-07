using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Header("’e‚Ì‘¬“x")]
    protected float _speed;
    [SerializeField]
    protected int _power;

    protected Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _Move();
    }

    protected void _Move()
    {
        _rigid.linearVelocity = transform.up * _speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().IsDamage()) return;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy"){
            Destroy(gameObject);
        }
    }

    public int GetPower()
    {
        return _power;
    }

}