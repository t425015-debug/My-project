using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HPIcon : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [SerializeField, Header("HPアイコン")]
    private GameObject _hpIcon;

    private int _beforeHP;
    private List<GameObject> _hpIconList;

    private void Start()
    {
        _hpIconList = new List<GameObject>();

        _CreateIcon();
        _beforeHP = _player.GetHP();
    }

    private void _CreateIcon()
    {
        for (int i = 0; i < _player.GetHP(); i++)
        {
            GameObject icon = Instantiate(_hpIcon);
            icon.transform.SetParent(transform);
            _hpIconList.Add(icon);
        }
    }

    void Update()
    {
        _ShowHPIcon();
    }
    private void _ShowHPIcon()
    {
        if (_beforeHP == _player.GetHP()) return;

        // HPが増えた場合
        while (_hpIconList.Count < _player.GetHP())
        {
            GameObject icon = Instantiate(_hpIcon, transform);
            _hpIconList.Add(icon);
        }

        // 表示更新
        for (int i = 0; i < _hpIconList.Count; i++)
        {
            _hpIconList[i].SetActive(i < _player.GetHP());
        }

        _beforeHP = _player.GetHP();
    }
}
