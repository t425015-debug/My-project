using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    enum _Action
    {
        None,
        _Shop,
        _ShopYesNo,
        PowerUp,
        PowerYesNo,
        _Bag,
    }

    [SerializeField] TextMeshProUGUI _mainTextBox;
    [SerializeField] GameObject _subTextBox;
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] List<TextMeshProUGUI> _moveTexts;
    [SerializeField] Color _highlightColor;
    [SerializeField] GameObject _gameObject;
    [SerializeField] List<TextMeshProUGUI> _itemTexts;
    [SerializeField] List<Sprite> _sprite;
    [SerializeField] List<TextMeshProUGUI> _itemDiscriptions;
    [SerializeField] GameObject _shopWindow;
    [SerializeField] List<TextMeshProUGUI> _powerUpTexts;
    [SerializeField] GameObject _powerUpWindow;
    [SerializeField] Bag _bag;
    [SerializeField] ItemDataBase _base;
    [SerializeField] List<TextMeshProUGUI> _subTexts2;
    [SerializeField] GameObject _subText2;

    private int _gold;
    private int _currentMove; // 0:左上, 1: 右上, ２:左下, 3:右下
    private Vector2 _input;
    private int _currentItem;
    private _Action _action;
    private string _mainText;
    private int _currentPowerUp;
    private int _currentYesNo;
    private bool _isYesNo;
    void Start()
    {
        _gold = 0;
        _currentMove = 0;
        _currentItem = 0;
        _currentPowerUp = 0;
        _currentYesNo = 0;
        _action = _Action.None;
        _subText2.SetActive(false);
        _isYesNo = false;
        _mainText = "いらっしゃい!";
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

        _goldText.text = $"{_gold}";
        if (_action == _Action.None)
        {
            HandleActionSelection();
            _MoveSelectColor();


            switch (_currentMove)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action._Shop;
                        return;
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action.PowerUp;
                        return;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action._Bag;
                        return;
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Map");
                        return;
                    }
                    break;
            }
        }
            if (_action == _Action._Shop) _Shop();
            if (_action == _Action.PowerUp) _PowerUp();
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

        _currentMove = Mathf.Clamp(_currentMove, 0, 3);
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
    }

    void _PowerUpSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentPowerUp++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentPowerUp--;
        }

        _currentPowerUp = Mathf.Clamp(_currentPowerUp, 0, _powerUpTexts.Count - 1);
    }

    void _YesNoSelection()
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
        for (int i = 0; i <= 3; i++)
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

    private void _PowerUpSelectColor()
    {
        for (int i = 0; i <= _powerUpTexts.Count - 1; i++)
        {
            if (_currentPowerUp == i)
            {
                _powerUpTexts[i].color = _highlightColor;
                _powerUpTexts[i].fontSize = 55;
            }
            else
            {
                _powerUpTexts[i].color = Color.white;
                _powerUpTexts[i].fontSize = 50;
            }
        }
    }

    private void _YesNoSelectColor()
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
                Debug.Log("a");
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
                    case 0: _base.GetItem(_currentItem); break;
                    case 1: _subText2.SetActive(false); break;
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

    private void _PowerUp()
    {
        _mainText = "どれを強化する？";
        _powerUpWindow.SetActive(true);
        _PowerUpSelection();
        _PowerUpSelectColor();
        if (Input.GetKeyDown(KeyCode.X))
        {
            _powerUpWindow.SetActive(false);
            _action = _Action.None;

        }
    }

    private void _Bag()
    {

    }

}
