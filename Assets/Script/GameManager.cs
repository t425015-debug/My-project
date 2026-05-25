using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("遅くなる時間")]
    private float _deadEffectTimeScale;
    [SerializeField,Header("時間を元に戻す時間")]
    private float _deadEffectTime;
    [SerializeField, Header("ゲームオーバー")]
    private GameObject _gameOver;

    private bool _bShowResult;

    void Start()
    {
        _bShowResult = false;
    }

    void Update()
    {
        if (_bShowResult)
        {
            OnRetry();
        }
    }

    public void DeadEffect()
    {
        StartCoroutine(Slow());
    }

    IEnumerator Slow()
    {
        Time.timeScale = _deadEffectTimeScale;

        yield return new WaitForSecondsRealtime(_deadEffectTime);

        Time.timeScale = 1.0f;
        _gameOver.SetActive(true);
        _bShowResult = true;
    }

    public bool IsShowResult()
    {
        return _bShowResult;
    }

    public void OnRetry()
    {
        if (!IsShowResult()) return;
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
