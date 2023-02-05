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
        _animator.Play("AppearTrigger");
    }

    public void PlayerOut()
    {
        _animator.Play("DisappearTrigger");
    }
}
