using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    private float _time;
    void Start()
    {
        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;
    }
}
