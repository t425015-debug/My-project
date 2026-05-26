using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    enum _Action
    {
        None,
        _Shop,
        PowerUp,
        _Bag,
    }

    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] List<TextMeshProUGUI> _moveTexts;
    [SerializeField] Color _highlightColor;
    [SerializeField] GameObject _gameObject;
    [SerializeField] List<TextMeshProUGUI> _itemTexts;
    [SerializeField] List<Sprite> _sprite;
    [SerializeField] List<TextMeshProUGUI> _itemDiscriptions;
    [SerializeField] GameObject _shopWindow;

    private int _gold;
    private int _currentMove; // 0:左上, 1: 右上, ２:左下, 3:右下
    private Vector2 _input;
    private int _currentItem;
    private _Action _action;

    void Start()
    {
        _gold = 0;
        _currentMove = 0;
        _currentItem = 0;
        _action = _Action.None;
    }

    void Update()
    {
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
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action.PowerUp;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _action = _Action._Bag;
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Map");
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentItem++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentItem--;
        }

        _currentItem = Mathf.Clamp(_currentItem, 0, _itemTexts.Count - 1);
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
        //selectMoveが0の時はmoveText[0]の色を青に変える、それ以外を黒
        //selectMoveが1の時はmoveText[1]の色を青に変える、それ以外を黒
        //selectMoveが2の時はmoveText[2]の色を青に変える、それ以外を黒
        //selectMoveが3の時はText[3]の色を青に変える、それ以外を黒
        // actionTexts[0]かactionTexts[1]
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

    private void _Shop()
    {
        _shopWindow.SetActive(true);
        _moveTexts[_currentMove].color = Color.black;
        _ShopItemSelection();
        _ShopItemSelectColor();
        if (Input.GetKeyDown(KeyCode.X)){
            _shopWindow.SetActive(false);
            _action = _Action.None;
        }
    }

private void _PowerUp()
    {

    }

    private void _Bag()
    {

    }

}
