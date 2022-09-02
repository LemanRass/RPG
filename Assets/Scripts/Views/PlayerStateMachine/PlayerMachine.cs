using UnityEngine;

namespace Views.PlayerStateMachine
{
    public interface IPlayerStateMachine
    {
        public CameraView cameraView { get; }
        public Animator animator { get; }
        public Transform transform { get; }
        public GameObject gameObject { get; }
        public PlayerMachine machine { get; }
        public PlayerUnit unit { get; }
        public MovePointView movePointView { get; }
        public EnemyPointView enemyPointView { get; }
    }
    
    public class PlayerMachine
    {
        private PlayerState _playerState;

        public PlayerMachine(IPlayerStateMachine player)
        {
            ChangeState(new PlayerIdleState(player));
        }

        public void ChangeState(PlayerState playerState)
        {
            _playerState?.OnExit();
            _playerState = playerState;
            _playerState.OnEnter();
        }

        public void Update()
        {
            _playerState.OnUpdate();
        }
    }
}