using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Title : MonoBehaviour
{
    [SerializeField]
    private AudioClip _bgm;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(_bgm);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySE("ƒ^ƒCƒgƒ‹Œˆ’èSE");
            StartCoroutine(a());
            
        }
    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Map");
    }
}
