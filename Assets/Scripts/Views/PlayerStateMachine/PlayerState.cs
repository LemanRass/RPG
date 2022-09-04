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


        protected bool CheckForChase(out Unit target)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _player.cameraView.camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 100.0f, ~LayerMask.NameToLayer("Enemies")))
                {
                    target = hit.transform.GetComponent<Unit>();
                    if (target != null)
                    {
                        if (target == _player.unit.target.selected)
                            return true;
                        
                        _player.unit.target.Select(target);
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_player.unit.target.selected != null)
                    _player.unit.target.Clear();
            }

            target = null;
            return false;
        }
        
        protected bool CheckForMove(out Vector3 point)
        {
            point = Vector3.zero;
            
            if (Input.GetMouseButtonDown(1))
            {
                var ray = _player.cameraView.camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 100.0f))
                {
                    if (!hit.transform.CompareTag("Ground"))
                        return false;
                    
                    point = hit.point;
                    _player.movePointView.Show(point);
                    return true;
                }
            }

            return false;
        }
    }
}