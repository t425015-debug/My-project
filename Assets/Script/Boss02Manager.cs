using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Boss02Manager : MonoBehaviour
{
    [SerializeField, Header("ワーニングオブジェクト")]
    private GameObject _warningObject;

    [SerializeField, Header("ワーニング表示開始までの時間")]
    private float _warningStartTime;

    [SerializeField, Header("表示時間")]
    private float _warningTime;

    [SerializeField, Header("ボス登場までの時間")]
    private float _bossStartTime;

    [SerializeField, Header("暗転するまでの時間")]
    private float _blackStartTime;

    [SerializeField, Header("暗転の時間")]
    private float _blackTime;


    [SerializeField, Header("ボスオブジェクト")]
    private GameObject _boss;

    [SerializeField, Header("ボススタートオブジェクト")]
    private GameObject _bossStart;

    [SerializeField, Header("暗転")]
    private GameObject _bossBlack;

    private Transform _canvas;
    void Start()
    {
        _canvas = FindFirstObjectByType<Canvas>().transform;
        StartCoroutine(_Warning());
    }

    void Update()
    {
    }

    IEnumerator _Warning()
    {
        yield return new WaitForSeconds(_warningStartTime);
        GameObject W = Instantiate(_warningObject, _canvas);


        yield return new WaitForSeconds(_warningTime);
        Destroy(W);
        yield return new WaitForSeconds(_warningTime);
        StartCoroutine(_SpownBoss());
    }

    IEnumerator _SpownBoss()
    {
        yield return new WaitForSeconds(_bossStartTime);
        GameObject _BOSSSTART = Instantiate(_bossStart);
        yield return new WaitForSeconds(_blackStartTime);
        GameObject B = Instantiate(_bossBlack, _canvas);
        yield return new WaitForSeconds(_blackTime);
        Destroy(B);
        Destroy(_BOSSSTART);
        Instantiate(_boss);
    }
}
