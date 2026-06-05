using UnityEngine;
using UnityEngine.InputSystem.Switch;

[CreateAssetMenu]
public class PlayerStatas : ScriptableObject
{
    [SerializeField, Header("初期スピード")]
    public  float _speed = 10;
    [SerializeField, Header("初期クールタイム")]
    public  float _coolTime = 0.5f;
    [SerializeField, Header("初期レベル")]
    public  int _level = 1;
    [SerializeField, Header("初期HP")]
    public int _hp = 5;
    [SerializeField, Header("初期攻撃力")]
    public int _power = 1;
    [SerializeField, Header("初期攻撃力レベル")]
    public int _powerLevel = 1;
    [SerializeField, Header("初期クールタイムレベル")]
    public int _coolTimeLevel = 1;
    [SerializeField, Header("初期体力レベル")]
    public int _hpLevel = 1;
    [SerializeField, Header("所持金")]
    public  int _money = 0;

}
