using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype__Sollner_.Inventory
{
    public static class UIHelper
    {
        private static PointerEventData _pointerEventData;
        private static List<RaycastResult> _rayCastResults = new List<RaycastResult>();
        
        public static GameObject GetPointerUI()
        {
            _pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            _rayCastResults.Clear();
            
            EventSystem.current.RaycastAll(_pointerEventData, _rayCastResults);

            if (_rayCastResults.Count > 0)
                return _rayCastResults[0].gameObject;

            return null;
        }
    }
}