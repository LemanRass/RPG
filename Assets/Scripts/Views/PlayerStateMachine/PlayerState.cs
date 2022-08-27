using UnityEngine;

namespace Views.PlayerStateMachine
{
    public abstract class PlayerState
    {
        protected readonly IPlayerStateMachine _player;
        
        protected PlayerState(IPlayerStateMachine player)
        {
            _player = player;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();

        protected bool CheckForMove(out Vector3 point)
        {
            point = Vector3.zero;
            
            if (Input.GetMouseButtonDown(1))
            {
                var ray = _player.cameraView.camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 100.0f, LayerMask.NameToLayer("Ground")))
                {
                    point = hit.point;
                    _player.transform.forward = Vector3.Normalize(point - _player.transform.position);
                    _player.movePointView.Show(point);
                    return true;
                }
            }

            return false;
        }
    }
}