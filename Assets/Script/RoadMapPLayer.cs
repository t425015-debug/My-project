using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class RoadMapPLayer : PlayerStatas
{ 
    [SerializeField]  float moveSpeed;
    [SerializeField, Header("PlanetName")]
    private TextMeshProUGUI _planetName;
    [SerializeField, Header("PlanetSprite")]
    private SpriteRenderer _planetSptiteRenderer;
    [SerializeField, Header("パワーテキスト")]
    private TextMeshProUGUI _playerPowerText;
    [SerializeField, Header("クールタイムテキスト")]
    private TextMeshProUGUI _playerCoolTimeText;
    [SerializeField, Header("体力テキスト")]
    private TextMeshProUGUI _playerHpText;
    [SerializeField, Header("惑星の名前リスト")]
    private List<TextMeshProUGUI> _planetNameTexts;
    [SerializeField, Header("惑星の名前stringリスト")]
    private List<string> _planetNamesTexts;
    [SerializeField, Header("アイテムテキスト")]
    private TextMeshProUGUI _itemText;
    [SerializeField, Header("ウィンドウ")]
    private GameObject _planetWindow;
    [SerializeField, Header("惑星画像リスト")]
    private List<Sprite> _planetSprites;
    [SerializeField, Header("惑星の名前リストオブジェクト")]
    private GameObject _planetNameGameObject;
    [SerializeField, Header("ウィンドウプラネット")]
    private GameObject _planetWindowGameObject;

    [SerializeField, Header("はてな３")]
    private GameObject _hatena3;
    [SerializeField, Header("はてな4")]
    private GameObject _hatena4;
    [SerializeField, Header("はてな5")]
    private GameObject _hatena5;

    [SerializeField, Header("惑星オブジェクト３")]
    private GameObject _planetObject3;
    [SerializeField, Header("惑星オブジェクト4")]
    private GameObject _planetObject4;
    [SerializeField, Header("惑星オブジェクト5")]
    private GameObject _planetObject5;

    [SerializeField, Header("惑星名前3")]
    private TextMeshProUGUI _planetNameText3;
    [SerializeField, Header("惑星名前4")]
    private TextMeshProUGUI _planetNameText4;
    [SerializeField, Header("惑星名前5")]
    private TextMeshProUGUI _planetNameText5;


    public static RoadMapPLayer Instance;


    bool isMoving;
    bool isWindowOpen;
    public static bool _isCleer2;
    public static bool _isCleer3;
    public static bool _isCleer4;


    Vector2 input;
    int lane = 0;
    Vector3 pos;
    private int _currentPlanet;


    float[] laneX =
    {
        -7.3f,
        -3.65f,
        0f,
        3.65f,
        7.3f
    };

    private void Awake()
    {
        Instance = this;
    }

    public int Score;

private void _LaneMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane--;
            _currentPlanet--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane++;
            _currentPlanet++;
        }

        // 範囲制限
        lane = Mathf.Clamp(lane, 0, 4);
        _currentPlanet = Mathf.Clamp(_currentPlanet, 0, 4);

        // レーン位置へ移動
        pos.x = laneX[lane];
    }


    public void HandleUpdate()
    {
        if (!isMoving)
        {
            _LaneMove();
            //キーボードの入力方向に動く
            input.x = Input.GetAxisRaw("Horizontal");

            // 斜め移動

            if (input != Vector2.zero)
            {
                Vector2 targetPos = transform.position;
                targetPos.x =  pos.x;
                StartCoroutine(Move(targetPos));
            }
        }
    }

   

    //コルーチンを使って徐々に目的地に近づける
    IEnumerator Move(Vector3 targetPos)
    {
        //移動中は入力を受け付けたくない

        isMoving = true;

        //targetPosとの差があるなら繰り返す
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //targetposに近づける
            transform.position = Vector3.MoveTowards(
                transform.position,          //現在の場所
                targetPos,                   //目的地
                moveSpeed * Time.deltaTime)　 //近づけるスピード
                ;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }


    void Start()
    {
        isMoving = false;
        pos = transform.position;
        _currentPlanet = 0;
        _planetSptiteRenderer = _planetWindowGameObject.GetComponent<SpriteRenderer>();
        _UnlockStage();
    }

    void Update()
    {
        if (!isWindowOpen)
        {
            HandleUpdate();

        }  
        _StageSelect();
    }

    private void _StageSelect()
    {
        if (isWindowOpen && Input.GetKeyDown(KeyCode.X))
        {
            _planetWindow.SetActive(false);

            _planetNameGameObject.SetActive(true);

            isWindowOpen = false;

            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        { 

            // ウィンドウが閉じているなら開く
            if (!isWindowOpen)
            {
                _Window(_planetSprites[_currentPlanet], _planetNameTexts[_currentPlanet]);
                isWindowOpen = true;
            }
            // 開いているならシーン移動
            else
            {
                _planetNameGameObject.SetActive(true);
                SceneManager.LoadScene($"Stage{_currentPlanet + 1}");
            }
        }
    }

    private void _Window(Sprite _planetSprite, TextMeshProUGUI _planetText)
    {
        _planetWindow.SetActive(true);
        _planetNameGameObject.SetActive(false);
        _playerPowerText.text = $"攻撃力:{_power}(Lv:{_powerLevel})";
        _playerCoolTimeText.text = $"Cタイム:{_coolTime}秒(Lv:{_coolTimeLevel})";
        _playerHpText.text = $"体力:{_hp}(Lv:{_hpLevel})";
        _planetName.text = _planetText.text;
        _planetSptiteRenderer.sprite = _planetSprite;
    }

    private void _UnlockStage()
    {
        if (_isCleer2)
        {
            _hatena3.SetActive(false);
            _planetObject3.SetActive(true);
            _planetNameText3.text = $"{_planetNamesTexts[3]}";
        }
        if (_isCleer3)
        {
            _hatena4.SetActive(false);
            _planetObject4.SetActive(true);
            _planetNameText4.text = $"{_planetNamesTexts[4]}";
        }
        if (_isCleer4)
        {
            _hatena5.SetActive(false);
            _planetObject5.SetActive(true);
            _planetNameText5.text = $"{_planetNamesTexts[5]}";
        }
    }
}
