using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Home
{

    [RequireComponent(typeof(Animator))]
    public class HomeGraphics : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField] private float _distance;
        [SerializeField] private Player _player;
        private static readonly int WakeStop = Animator.StringToHash("WakeStop");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            float lerp = distance / _distance;

            lerp = Mathf.Clamp(lerp, 0, 1f);
            
            _animator.SetFloat(WakeStop, lerp);
        }
    }
}
