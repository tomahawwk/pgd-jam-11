using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class GraphicsVodyanoi : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayerIn()
    {
        if (_animator.GetBool("Bye"))
        {
            _animator.SetBool("Bye", false);
            _animator.SetBool("Appear", true);
        }
        else
        {
            _animator.SetBool("Appear", false);
            _animator.SetBool("Idle", true);
        }

    }

    public void PlayerOut()
    {
        if (_animator.GetBool("Idle"))
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Bye", true);
        }
    }
}
