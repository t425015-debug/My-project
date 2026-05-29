using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class PowerUp : PlayerStatas
{
    [SerializeField, Header("パワーアップ選択テキスト")]
    private List<TextMeshProUGUI> _powerUpSelectTexts;
    [SerializeField, Header("パワーアップ選択オブジェクト")]
    private GameObject _powerUpObject;
    [SerializeField, Header("パワーレベルテキスト")]
    private TextMeshProUGUI _powerLeveltext;
    [SerializeField, Header("パワーレベルリスト")]
    private List<int> _powerLevelLists;
    [SerializeField, Header("パワー数値テキスト")]
    private TextMeshProUGUI _powerIndexText;
    [SerializeField, Header("クールタイムレベルリスト")]
    private List<float> _coolTimeLevelLists;
    [SerializeField, Header("クールタイムレベルテキスト")]
    private TextMeshProUGUI _coolTimeLevelText;
    [SerializeField, Header("クールタイム数値テキスト")]
    private TextMeshProUGUI _coolTimeIndexText;
    [SerializeField, Header("体力レベルテキスト")]
    private TextMeshProUGUI _hpLevelText;
    [SerializeField, Header("体力レベルリスト")]
    private List<int> _hpLevelLists;
    [SerializeField, Header("体力数値テキスト")]
    private TextMeshProUGUI _hpIndextext;
    [SerializeField, Header("レベルリスト")]
    private List<TextMeshProUGUI> _levelTexts;
    [SerializeField, Header("数値リスト")]
    private List<TextMeshProUGUI> _indexTexts;

    private int _currentPowerUpSelect;
    private int _currentYesNo;
    private Stage1 _stage1;


    private bool _isYesNo;

    void Start()
    {
        _stage1 = FindFirstObjectByType<Stage1>();
        _currentPowerUpSelect = 0;
        _currentYesNo = 1;
        _isYesNo = false;

        _powerLevel = 1;
        _coolTimeLevel = 1;
        _hpLevel = 1;
    }

    public void _PowerUp()
    {
        _stage1._mainText = "どれを強化する？";
        _powerUpObject.SetActive(true);
        _powerIndexText.text = $"{_power}";
        _coolTimeIndexText.text = $"{_coolTime}秒";
        _hpIndextext.text = $"{_hp}";
 
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
            _YesNoSelection();
            _YesNoSelectColor();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_currentYesNo == 0)
                {
                    switch (_currentPowerUpSelect)
                    {
                        case 0:
                            _powerLevel++;
                            _power = _powerLevelLists[_powerLevel - 1];
                            _powerIndexText.text = $"{_power}";
                            _powerLeveltext.text = $"Lv:{_powerLevel}"; break;

                        case 1:
                            _coolTimeLevel++;
                            _coolTime = _coolTimeLevelLists[_coolTimeLevel - 1];
                            _coolTimeIndexText.text = $"{_coolTime}";
                            _coolTimeLevelText.text = $"Lv:{_coolTimeLevel}"; break;

                        case 2: _hpLevel++;
                            _hp = _hpLevelLists[_hpLevel - 1];
                            _hpIndextext.text = $"{_hp}";
                            _hpLevelText.text = $"Lv:{_hpLevel}"; break;
                    }
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
                _powerUpSelectTexts[i].fontSize = 45;

                _levelTexts[i].color = _stage1._highlightColor;
                _levelTexts[i].fontSize = 45;

                _indexTexts[i].color = _stage1._highlightColor;
                _indexTexts[i].fontSize = 45;
            }
            else
            {
                _powerUpSelectTexts[i].color = Color.white;
                _powerUpSelectTexts[i].fontSize = 40;

                _levelTexts[i].color = Color.white;
                _levelTexts[i].fontSize = 40;

                _indexTexts[i].color = Color.white;
                _indexTexts[i].fontSize = 40;
            }
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

        _currentYesNo = Mathf.Clamp(_currentYesNo, 0, _stage1._subTexts2.Count - 1);
    }

    public void _YesNoSelectColor()
    {
        for (int i = 0; i <= _stage1._subTexts2.Count - 1; i++)
        {
            if (_currentYesNo == i)
            {
                _stage1._subTexts2[i].color = _stage1._highlightColor;
                _stage1._subTexts2[i].fontSize = 35;
            }
            else
            {
                _stage1._subTexts2[i].color = Color.black;
                _stage1._subTexts2[i].fontSize = 30;
            }
        }
    }

}
