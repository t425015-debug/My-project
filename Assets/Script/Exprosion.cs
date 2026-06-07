using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Exprosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(_Destroy());
        AudioManager.Instance.PlaySE("”š”­SE");
    }

    private IEnumerator _Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
