using UnityEngine;

namespace UI.Tools.UIDragDrop
{
    public class DragNDrop : MonoBehaviour
    {
        public static DragNDrop instance { get; private set; }

        private void Awake() => instance = this;
    }
}