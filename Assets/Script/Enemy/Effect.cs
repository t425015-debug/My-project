using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _Dead();
    }

    private void _Dead()
    {
        if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
