using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI[] _moveTexts;
    [SerializeField] Color _highlightColor;

    private int _gold;
    private int _currentMove; // 0:左上, 1: 右上, ２:左下, 3:右下
    private Vector2 _input;


    void Start()
    {
        _gold = 0;
        _currentMove = 0;
    }

    void Update()
    {
        _goldText.text = $"{_gold}";
        _ActionSelector();
        HandleMoveSelection();
        switch (_currentMove)
        {
            case 0:
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("Stage1");
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("Stage2");
                }
                break;
            case 2:
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("Stage3");
                }
                break;
            case 3:
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("Map");
                }
                break;
        }

    }

    private void _ActionSelector()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");


    }

    void HandleMoveSelection()
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

        UpdateMoveSelection();
    }


    // 選択中にアクションの色を変える

    public void UpdateMoveSelection()
    {
        //selectMoveが0の時はmoveText[0]の色を青に変える、それ以外を黒
        //selectMoveが1の時はmoveText[1]の色を青に変える、それ以外を黒
        //selectMoveが2の時はmoveText[2]の色を青に変える、それ以外を黒
        //selectMoveが3の時はText[3]の色を青に変える、それ以外を黒
        // actionTexts[0]かactionTexts[1]
        for (int i = 0; i <= 3; i++)
        {
            if (_currentMove == i)
            {
                _moveTexts[i].color = _highlightColor;
            }
            else
            {
                _moveTexts[i].color = Color.black;
            }
        }

    }

    private void _Shop()
    {

    }

    private void _PowerUp()
    {

    }

    private void _EnemyList()
    {

    }

    private void _Return()
    {

    }
}
