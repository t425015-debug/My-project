using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    public enum _Action
    {
        None,
        _Shop,
        PowerUp,
        _Bag,
    }

    [SerializeField]
    private PlayerStatas _playerStatas;

    [SerializeField,Header("メインテキスト")]
    public TextMeshProUGUI _mainTextBox;
    [SerializeField, Header("行動テキストオブジェクト")]
    private GameObject _subTextBox;
    [SerializeField, Header("お金のテキスト")]
    private TextMeshProUGUI _goldText;
    [SerializeField, Header("行動テキスト")] 
    private List<TextMeshProUGUI> _moveTexts;
    [SerializeField, Header("選択カラー")]
    public Color _highlightColor;
    [SerializeField, Header("アイテム画像")]
    private GameObject _itemPhoto;
    [SerializeField, Header("アイテム名テキスト")]
    private List<TextMeshProUGUI> _itemTexts;
    [SerializeField, Header("ショップウィンドウオブジェクト")]
    private GameObject _shopWindow;
    [SerializeField, Header("バッグオブジェクト")] 
    private Bag _bag;
    [SerializeField, Header("アイテムデータ")]
    private ItemDataBase _base;
    [SerializeField,Header("はい、いいえテキスト")]
    public List<TextMeshProUGUI> _subTexts2;
    [SerializeField, Header("はい、いいえテキストオブジェクト")]
    public GameObject _subText2;
    [SerializeField, Header("所持数テキスト")]
    private TextMeshProUGUI _haveItemCount;
    [SerializeField, Header("PowerUpWindow")]
    private PowerUp _powerUp;

    private int _currentMove; // 0:左上, 1: 右上, ２:左下, 3:右下
    private int _currentItem;
    public _Action _action;
    public string _mainText;
    private int _currentYesNo;
    private bool _isYesNo;
    private SpriteRenderer _itemSpriteRenderer;


    void Start()
    {
        _currentMove = 0;
        _currentItem = 0;
        _currentYesNo = 0;
        _action = _Action.None;
        _subText2.SetActive(false);
        _isYesNo = false;
        _mainText = "いらっしゃい!";
        _itemSpriteRenderer = _itemPhoto.GetComponent<SpriteRenderer>();
        _shopWindow.SetActive(false);
    }

    void Update()
    {
        if (_action != _Action.None)
        {
            _subTextBox.SetActive(false);
        }
        else {
            _subTextBox.SetActive(true);
            _mainText = "いらっしゃい!";

        }
        _mainTextBox.text = $"{_mainText}";

        _goldText.text = $"{_playerStatas._money}";
        if (_action == _Action.None)
        {
            HandleActionSelection();
            _MoveSelectColor();


            switch (_currentMove)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action.PowerUp;
                        return;
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Map");
                        return;
                    }
                    break;
            }
        }
            if (_action == _Action._Shop) _Shop();
            if (_action == _Action.PowerUp) _powerUp._PowerUp();
            if (_action == _Action._Bag) _Bag();
        
    }


    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentMove++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentMove--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentMove -= 2;
        }

        _currentMove = Mathf.Clamp(_currentMove, 0, 1);
    }

    void _ShopItemSelection()
    {
        if (_isYesNo == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _currentItem++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentItem--;
            }
        }
        _currentItem = Mathf.Clamp(_currentItem, 0, _itemTexts.Count - 1);

        _mainTextBox.text = _base.items[_currentItem]._explaningText;
        _itemSpriteRenderer.sprite = _base.items[_currentItem]._itemSprite;

        PocketItem haveItem = _bag._items.Find(
         item => item._name ==
         _base.items[_currentItem]._name
         );

        if (haveItem != null)
        {
            _haveItemCount.text =
                $"所持数：{haveItem._count}";
        }
        else
        {
            _haveItemCount.text = "所持数：0";
        }
    }


    public void _YesNoSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentYesNo++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentYesNo--;
        }

        _currentYesNo = Mathf.Clamp(_currentYesNo, 0, _subTexts2.Count - 1);
    }


    private void _MoveSelectColor()
    {
        //selectMoveが0の時はmoveText[0]の色を青に変える、それ以外を黒
        //selectMoveが1の時はmoveText[1]の色を青に変える、それ以外を黒
        //selectMoveが2の時はmoveText[2]の色を青に変える、それ以外を黒
        //selectMoveが3の時はText[3]の色を青に変える、それ以外を黒
        // actionTexts[0]かactionTexts[1]
        for (int i = 0; i <= 1; i++)
        {
            if (_currentMove == i && _action == _Action.None)
            {
                _moveTexts[i].color = _highlightColor;
                _moveTexts[i].fontSize = 33;
            }
            else
            {
                _moveTexts[i].color = Color.black;
                _moveTexts[i].fontSize = 30;
            }
        }

    }

    private void _ShopItemSelectColor()
    {
        for (int i = 0; i <= _itemTexts.Count - 1; i++)
        {
            if (_currentItem == i)
            {
                _itemTexts[i].color = _highlightColor;
                _itemTexts[i].fontSize = 55;
            }
            else
            {
                _itemTexts[i].color = Color.white;
                _itemTexts[i].fontSize = 50;
            }
        }

    }


    public void _YesNoSelectColor()
    {
        for (int i = 0; i <= _subTexts2.Count - 1; i++)
        {
            if (_currentYesNo == i)
            {
                _subTexts2[i].color = _highlightColor;
                _subTexts2[i].fontSize = 35;
            }
            else
            {
                _subTexts2[i].color = Color.black;
                _subTexts2[i].fontSize = 30;
            }
        }
    }


    private void _Shop()
    {
        _mainText = "何を買う？";
        _shopWindow.SetActive(true);
        if (!_isYesNo)
        {
            _ShopItemSelection();
            _ShopItemSelectColor();

            if (!_isYesNo && Input.GetKeyDown(KeyCode.Space))
            {
                _isYesNo = true;
                _subText2.SetActive(true);
                _currentYesNo = 1;

                return;
            }
        }

        if (_isYesNo)
        {
            _YesNoSelection();
            _YesNoSelectColor();

            if (Input.GetKeyDown(KeyCode.Space))
            {


                switch (_currentYesNo)
                {
                    case 0: _bag._ItemGet(_base.GetItem(_currentItem)); break;
                    case 1:  break;
                }

                // YES/NO閉じる
                _subText2.SetActive(false);
                _isYesNo = false;

                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _shopWindow.SetActive(false);
            _action = _Action.None;
        }
    }


    private void _Bag()
    {

    }

}
