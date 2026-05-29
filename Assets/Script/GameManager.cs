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
        Pose,
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

    [SerializeField, Header("ポーズウィンドウ")]
    private GameObject _poseWindow;

    private bool _bShowResult;
    private ResultMode _resultMode;
    private BossDeadEffect _bossDeadEffect;

    void Start()
    {
        _bShowResult = false;
        _resultMode = ResultMode.None;
        _bossDeadEffect = null;
    }
    void Update()
    {
        _Pose();

        if (_bShowResult)
        {
            OnRetry();
            _ShowGameClear();
        }
    }

    public void DeadEffect(ResultMode resultMode)
    {
        _resultMode = resultMode;
        StartCoroutine(Slow());
    }

    private void _ShowGameClear()
    {
        if (_bossDeadEffect == null) return;

        if (_bossDeadEffect.IsEnd())
        {
            _gameClear.SetActive(true);
            _bShowResult=true;
        }
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
            case ResultMode.GameOver:
                _gameOver.SetActive(true);
                _bShowResult = true;
                break;
            case ResultMode.GameClear: _bossDeadEffect = FindAnyObjectByType<BossDeadEffect>();break;
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

    public void _Pose()
    {
        if (Input.GetKeyDown(KeyCode.X) && _resultMode != ResultMode.Pose)
        {
            _resultMode = ResultMode.Pose;
            _poseWindow.SetActive(true);
            Time.timeScale = 0f;
        }

        // ポーズ解除
        if (_resultMode == ResultMode.Pose && Input.GetKeyDown(KeyCode.Space))
        {
            _poseWindow.SetActive(false);
            Time.timeScale = 1.0f;
            _resultMode = ResultMode.None;
        } else if(_resultMode == ResultMode.Pose && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Map");
            Time.timeScale = 1.0f;
        }
    }
}
