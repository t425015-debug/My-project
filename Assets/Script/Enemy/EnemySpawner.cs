using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("ボスマネージャー")]
    private GameObject _bossManager;

    [SerializeField, Header("敵オブジェクト")]
    private GameObject[] _enemy;
    [SerializeField, Header("敵を生成する時間")]
    private float[] _spawnTime;

    private float _spawnCount;
    private int _spawnNum;

    void Start()
    {
        _spawnCount = 0.0f;
        _spawnNum = 0;
    }

    void Update()
    {
        _Spawn();
    }

    private void _Spawn()
    {
        if (_spawnNum > _enemy.Length - 1) return;

        _spawnCount += Time.deltaTime;
        if(_spawnCount >= _spawnTime[_spawnNum])
        {
            Instantiate(_enemy[_spawnNum]);
            _spawnNum++;
            _spawnCount = 0.0f;
        }

        if (_spawnNum >= _enemy.Length)
        {
            _BossSporn();
            enabled = false; // 一度だけ実行
            return;
        }
    }

    private void _BossSporn()
    {
        _bossManager.SetActive(true);
    }
}
