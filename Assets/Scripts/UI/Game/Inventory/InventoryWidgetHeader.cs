using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Game
{
    public class InventoryWidgetHeader : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private Transform _windowTransform;
        
        private Vector2 _startMousePos;
        private Vector2 _startWindowPos;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _startMousePos = eventData.position;
            _startWindowPos = _windowTransform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _windowTransform.position = (eventData.position - _startMousePos) + _startWindowPos;
        }
    }
}