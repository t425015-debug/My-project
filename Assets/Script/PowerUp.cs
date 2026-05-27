using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField, Header("パワーアップ選択テキスト")]
    private List<TextMeshProUGUI> _powerUpSelectTexts;
    [SerializeField, Header("パワーアップ選択オブジェクト")]
    private GameObject _powerUpObject;


    private int _currentPowerUpSelect;
    private int _currentYesNo;
    private Stage1 _stage1;

    private bool _isYesNo;

    void Start()
    {
        _stage1 = FindFirstObjectByType<Stage1>();
        _currentPowerUpSelect = 0;
        _currentYesNo = 0;
        _isYesNo = false;
    }

    public void _PowerUp()
    {
        _stage1._mainText = "どれを強化する？";
        _powerUpObject.SetActive(true);
        if (!_isYesNo)
        {
            _PowerUpSelection();
            _PowerUpSelectColor();

            if (!_isYesNo && Input.GetKeyDown(KeyCode.Space))
            {
                _isYesNo = true;
                _stage1._subText2.SetActive(true);
                _currentYesNo = 1;
                return;
            }
        }

        if (_isYesNo)
        {
            _stage1._YesNoSelection();
            _stage1._YesNoSelectColor();

            if (Input.GetKeyDown(KeyCode.Space))
            {


                switch (_currentYesNo)
                {
                    case 0: break;
                    case 1: break;
                }

                // YES/NO閉じる
                _stage1._subText2.SetActive(false);
                _isYesNo = false;

                return;
            }
        }
            if (Input.GetKeyDown(KeyCode.X))
            {
                _powerUpObject.SetActive(false);
                _stage1._action = Stage1._Action.None;
            }
        

    }

    void _PowerUpSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentPowerUpSelect++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentPowerUpSelect--;
        }

        _currentPowerUpSelect = Mathf.Clamp(_currentPowerUpSelect, 0, _powerUpSelectTexts.Count - 1);
    }

    private void _PowerUpSelectColor()
    {
        for (int i = 0; i <= _powerUpSelectTexts.Count - 1; i++)
        {
            if (_currentPowerUpSelect == i)
            {
                _powerUpSelectTexts[i].color = _stage1._highlightColor;
                _powerUpSelectTexts[i].fontSize = 55;
            }
            else
            {
                _powerUpSelectTexts[i].color = Color.white;
                _powerUpSelectTexts[i].fontSize = 50;
            }
        }
    }
}
