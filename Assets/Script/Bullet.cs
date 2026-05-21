using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Header("’e‚Ì‘¬“x")]
    private float _speed;
    [SerializeField, Header("’e‚ÌˆÐ—Í")]
    private int _power;

    private Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _Move();
    }

    public void _Move()
    {
        _rigid.linearVelocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}