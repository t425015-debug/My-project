using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class PlayerStatas : MonoBehaviour
{
    [SerializeField, Header("初期スピード")]
    public static float _speed = 10;
    [SerializeField, Header("初期クールタイム")]
    public static float _coolTime = 0.5f;
    [SerializeField, Header("初期レベル")]
    public static int _level = 1;
    [SerializeField, Header("初期HP")]
    public static int _hp = 5;
    [SerializeField, Header("初期攻撃力")]
    public static int _power = 1;
    [SerializeField, Header("初期攻撃力レベル")]
    public static int _powerLevel = 1;
    [SerializeField, Header("初期クールタイムレベル")]
    public static int _coolTimeLevel = 1;
    [SerializeField, Header("初期体力レベル")]
    public static int _hpLevel = 1;
    [SerializeField, Header("所持金")]
    public static int _money = 0;

}
