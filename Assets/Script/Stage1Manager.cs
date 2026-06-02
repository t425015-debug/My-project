using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Stage1Manager : MonoBehaviour
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
    void Start()
    {
        _warningObject.SetActive(false);
        StartCoroutine(_Warning());
    }

    void Update()
    {
    }

    IEnumerator _Warning()
    {
        yield return new WaitForSeconds(_warningStartTime);
        _warningObject.SetActive(true);
        yield return new WaitForSeconds(_warningTime);
        _warningObject.SetActive(false );
        yield return new WaitForSeconds(_warningTime);
        StartCoroutine(_SpownBoss());
    }

    IEnumerator _SpownBoss()
    {
        yield return new WaitForSeconds(_bossStartTime);
        _bossStart.SetActive(true);
        yield return new WaitForSeconds(_blackStartTime); 
        _bossBlack.SetActive(true);
        yield return new WaitForSeconds(_blackTime);
        _bossBlack.SetActive(false);
        _bossStart.SetActive(false);
        Instantiate(_boss);
    }
}
