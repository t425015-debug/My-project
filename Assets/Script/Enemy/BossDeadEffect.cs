using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BossDeadEffect : MonoBehaviour
{
    [SerializeField, Header("爆発エフェクト")]
    private GameObject _deadEffect;
    [SerializeField, Header("エフェクトを出す範囲")]
    private float _effectRange;
    [SerializeField, Header("エフェクトを出す間隔")]
    private float _effectSpawnTime;
    [SerializeField, Header("エフェクトの時間")]
    private float _effectTIme;
    [SerializeField, Header("エフェクトサイズ")]
    private float _effectScale;
    [SerializeField, Header("ボスが消えてからの間")]
    private float _endTime;

    private float _effectTimeCount;
    private bool _bEnd;
    private CinemachineImpulseSource _shaker;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _bEnd = false;
        _shaker = FindAnyObjectByType<CinemachineImpulseSource>();
        _shaker.GenerateImpulse();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _effectTimeCount = 0f;
        StartCoroutine(_IESpawnEffect());
        StartCoroutine(_IEEnd());
    }

    void Update()
    {
        _EffectTime();
    }

    private void _EffectTime()
    {
        _effectTimeCount += Time.deltaTime;
        if(_effectTimeCount >= _effectTIme)
        {
            _spriteRenderer.enabled = false;
            StartCoroutine(_IEEnd());
        }
    }

    IEnumerator _IESpawnEffect() {
        {
            Vector2 effectPos = transform.position + new Vector3(Random.Range(-_effectRange, _effectRange), Random.Range(-_effectRange, _effectRange));
            GameObject effect = Instantiate(_deadEffect, effectPos, Quaternion.identity);
            effect.transform.localScale = new Vector3(_effectScale, _effectScale, _effectScale);

            while (_spriteRenderer.enabled)
            {
                yield return new WaitForSeconds(_effectSpawnTime);

                effectPos = transform.position + new Vector3(Random.Range(-_effectRange, _effectRange), Random.Range(-_effectRange, _effectRange));
                effect = Instantiate(_deadEffect, effectPos, Quaternion.identity);
                effect.transform.localScale = new Vector3(_effectScale, _effectScale, _effectScale);
            }
        }

    }
    IEnumerator _IEEnd()
    {
        yield return new WaitForSeconds(_endTime);
            _bEnd = true;
    }

    public bool IsEnd()
    {
        return _bEnd;
    }
}
