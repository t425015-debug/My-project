using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum ResultMode
    {
        None,
        GameOver,
        GameClear,
    }

    [SerializeField, Header("遅くなる時間")]
    private float _deadEffectTimeScale;
    [SerializeField,Header("時間を元に戻す時間(Player死亡時)")]
    private float _deadEffectTime;
    [SerializeField, Header("時間を元に戻す時間(ボス死亡時)")]
    private float _bossDeadEffectTime;
    [SerializeField, Header("ゲームオーバー")]
    private GameObject _gameOver;
    [SerializeField, Header("ゲームクリア")]
    private GameObject _gameClear;

    private bool _bShowResult;
    private ResultMode _resultMode;

    void Start()
    {
        _bShowResult = false;
        _resultMode = ResultMode.None;
    }
    void Update()
    {
        if (_bShowResult)
        {
            OnRetry();
        }
    }

    public void DeadEffect(ResultMode resultMode)
    {
        _resultMode = resultMode;
        StartCoroutine(Slow());
    }

    IEnumerator Slow()
    {
        Time.timeScale = _deadEffectTimeScale;

        float deadEffectTime = 0f;
        switch (_resultMode)
        {
            case ResultMode.GameOver: deadEffectTime = _deadEffectTime; break;
            case ResultMode.GameClear: deadEffectTime = _bossDeadEffectTime; break;
        }
        yield return new WaitForSecondsRealtime(deadEffectTime);

        Time.timeScale = 1.0f;
        _bShowResult = true;

        switch (_resultMode)
        {
            case ResultMode.GameOver: _gameOver.SetActive(true); break;
            case ResultMode.GameClear: _gameClear.SetActive(true); break;
        }
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
