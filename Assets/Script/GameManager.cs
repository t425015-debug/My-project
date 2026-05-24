using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("’x‚­‚È‚éŽžŠÔ")]
    private float _deadEffectTimeScale;
    [SerializeField,Header("ŽžŠÔ‚ðŒ³‚É–ß‚·ŽžŠÔ")]
    private float _deadEffectTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DeadEffect()
    {
        StartCoroutine(Slow());
    }

    IEnumerator Slow()
    {
        Time.timeScale = _deadEffectTimeScale;
        yield return new WaitForSecondsRealtime(_deadEffectTime);
        Time.timeScale = 1.0f;
    }
}
