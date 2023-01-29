using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Triggers.Types
{
    public class TriggerEvent : OnPlayerTouchTrigger
    {
        [SerializeField] public UnityEvent OnPlayerEnter;
        public override void OnPlayerTouch()
        {
            OnPlayerEnter?.Invoke();
        }
    }
}