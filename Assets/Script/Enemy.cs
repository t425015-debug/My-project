using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    private GameObject _bullet;
    [SerializeField, Header("弾を発射する時間")]
    private float _shootTime;
    [SerializeField] private GameObject _player;

    private float _shootCount;

    void Start()
    {
        _shootCount = 0;
    }

    void Update()
    {
        _Shooting();
    }

    private void _Shooting()
    {
        _shootCount += Time.deltaTime;
        if (_shootCount < _shootTime) return;

        GameObject bulletObj = Instantiate(_bullet);
        bulletObj.transform.position = transform.position;
        Vector3 dir = _player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        _shootCount = 0.0f;
    }
}
