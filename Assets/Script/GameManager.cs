using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("クリア音")]
    private AudioClip _cleerSound;
    [SerializeField, Header("BGM")]
    private AudioClip _bgm;
    [SerializeField, Header("死亡BGM")]
    private AudioClip _DeadBGM;


    public enum ResultMode
    {
        None,
        GameOver,
        GameClear,
        Pose,
    }

    [SerializeField]
    private PlayerStatas _playerStatas;

    [SerializeField, Header("Player")]
    private PlayerController _player;
    [SerializeField]
    private HPIcon _hpIcon;


    [SerializeField, Header("遅くなる時間")]
    private float _deadEffectTimeScale;
    [SerializeField,Header("時間を元に戻す時間(Player死亡時)")]
    private float _deadEffectTime;
    [SerializeField, Header("時間を元に戻す時間(ボス死亡時)")]
    private float _bossDeadEffectTime;
    [SerializeField, Header("ゲームオーバー")]
    private GameObject _gameOver;
    [SerializeField, Header("ゲームクリアリザルト")]
    private GameObject _gameClearResult;
    [SerializeField, Header("ゲームクリアリザルトまでの時間")]
    private float _startGameClearResultTime;
    [SerializeField, Header("リザルトレベルテキスト")]
    private TextMeshProUGUI _resultLevelText;
    [SerializeField, Header("リザルトHPテキスト")]
    private TextMeshProUGUI _resultHPText;
    [SerializeField, Header("入手お金テキスト")]
    private TextMeshProUGUI _getMoneyText;
    [SerializeField, Header("倒した敵テキスト")]
    private TextMeshProUGUI _resultEnemyText;
    [SerializeField, Header("ポーズウィンドウ")]
    private GameObject _poseWindow;

    private bool _bShowResult;
    private bool _result;
    private ResultMode _resultMode;
    public BossDeadEffect _bossDeadEffect;
    private int _getMoney;
    private bool _isPlayClearSound;
    private bool _isShowGameClearStarted;

    public int _countBreakEnemy;
    public int _countPlayerLevel;
    public int _countPlayerHP;

    void Start()
    {
        _bShowResult = false;
        _result = false;
        _resultMode = ResultMode.None;
        _bossDeadEffect = null;
         _countBreakEnemy = 0;
         _countPlayerLevel = 0;
         _countPlayerHP = 0;
        _gameClearResult.SetActive(false);
        _isPlayClearSound = false;
        AudioManager.Instance.PlayBGM(_bgm);
    }
    void Update()
    {
        _Pose();

        if (_result && Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySE("決定SE");
            SceneManager.LoadScene("Map");
        }


        if (_bShowResult)
        {
            if (_bShowResult && !_isShowGameClearStarted)
            {
                _isShowGameClearStarted = true;
                StartCoroutine(_ShowGameClear());
            }

            if (_resultMode == ResultMode.GameOver &&
           Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.PlaySE("決定SE");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    public void DeadEffect(ResultMode resultMode)
    {
        _resultMode = resultMode;
        StartCoroutine(Slow());
    }

    private IEnumerator _ShowGameClear()
    {
        if (_bossDeadEffect == null) yield break;

        yield return new WaitForSeconds(_startGameClearResultTime);

        _player._canMove = false;
        if (!_isPlayClearSound)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlaySE("ゲームクリアSE");
            _isPlayClearSound = true;
        }

        if (_bossDeadEffect.IsEnd())
        {
            _result = true;
            _countPlayerLevel = _player._ResultCount();
            _countPlayerHP = _hpIcon._ResultHPCount();
            _resultLevelText.text = $"プレイヤーのレベル：{_countPlayerLevel}";
            _resultEnemyText.text = $"倒した敵の数：{_countBreakEnemy}";
            _resultHPText.text = $"プレイヤーの残りHP： {_countPlayerHP}";
            _getMoney = _countPlayerLevel * 200 + _countBreakEnemy * 100 + _countPlayerHP * 300;
            _playerStatas._money += _getMoney;
            _getMoneyText.text = $"{_getMoney}G";
            _gameClearResult.SetActive(true);
            _bShowResult = true;            
        }
    }
    IEnumerator Slow()
    {
        Time.timeScale = _deadEffectTimeScale;

        float deadEffectTime = 0f;

        AudioManager.Instance.PlayBGM(_DeadBGM);
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


    public void _Pose()
    {
        if (Input.GetKeyDown(KeyCode.X) && _resultMode != ResultMode.Pose)
        {
            _player._canMove = false;
            AudioManager.Instance.PauseBGM();
            _resultMode = ResultMode.Pose;
            _poseWindow.SetActive(true);
            Time.timeScale = 0f;
        }

        // ポーズ解除
        if (_resultMode == ResultMode.Pose && Input.GetKeyDown(KeyCode.Space))
        {
            _player._canMove = true;
            AudioManager.Instance.UnPauseBGM();
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
