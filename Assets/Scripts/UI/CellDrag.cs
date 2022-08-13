using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class CellDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Transform _beginDragTransform;
        private Vector2 _beginDragPosition;
        private Vector2 _beginMousePosition;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _beginMousePosition = Input.mousePosition;
            _beginDragPosition = transform.position;
            _beginDragTransform = transform.parent;

            transform.SetParent(DragNDrop.instance.transform);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position - _beginMousePosition + _beginDragPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Reset();
        }

        public void Reset()
        {
            transform.SetParent(_beginDragTransform);
            transform.position = _beginDragPosition;
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}