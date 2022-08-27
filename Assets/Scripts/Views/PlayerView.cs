using Cysharp.Threading.Tasks;
using UnityEngine;
using Views;
using Views.PlayerStateMachine;

public class PlayerView : MonoBehaviour, IPlayerStateMachine
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private PlayerUnit _player;
    [SerializeField] private MovePointView _movePointView;

    private PlayerMachine _machine;
    
    public CameraView cameraView => _cameraView;
    public Animator animator => _animator;
    public PlayerMachine machine => _machine;
    public PlayerUnit unit => _player;
    public MovePointView movePointView => _movePointView;


    private async void Start()
    {
        _movePointView.Init();
        await UniTask.WaitUntil(() => _player.isInitialized);
        _machine = new PlayerMachine(this);
    }

    private void Update()
    {
        _machine.Update();
    }

    /*[SerializeField] private ParticleSystem _moveMarkerPrefab;
    private ParticleSystem _moveMarker;
    
    private Coroutine _castSkillCoroutine;

    private void Awake()
    {
        _player.onInitialized += PlayerOnInitialized;
    }

    private void PlayerOnInitialized()
    {
        _moveMarker = Instantiate(_moveMarkerPrefab);
        
        _player.skills.skillCaster.onCastStarted += OnCastStarted;
        _player.skills.skillCaster.onCastCancelled += OnCastCancelled;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _player.talents.AddExperience(TalentType.VITALITY, Random.Range(1, 35));
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _player.talents.AddExperience(TalentType.INTELLECT, Random.Range(1, 35));
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _player.talents.AddExperience(TalentType.DEFENCE, Random.Range(1, 35));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _player.skills.skillCaster.Cancel();
        }

        if (Input.GetMouseButtonUp(0))
        {
            var ray = _cameraView.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100.0f, LayerMask.NameToLayer("Ground")))
            {
                _moveMarker.transform.position = hit.point;
                _moveMarker.Play();
                Debug.Log($"Click at: {hit.point}");
            }
        }
    }

    private void OnCastStarted(Skill skill)
    {
        _castSkillCoroutine = StartCoroutine(CastSkill(skill));
    }
    
    private void OnCastCancelled(Skill skill)
    {
        StopCoroutine(_castSkillCoroutine);
        _castSkillCoroutine = null;

        _animator.SetTrigger("CastCancel");
    }

    private IEnumerator CastSkill(Skill skill)
    {
        float duration = skill.config.castingDuration;
        float animLength = skill.config.anim.length;
        
        _animator.SetFloat("CastSpeed", animLength / duration);
        _animator.SetTrigger(skill.config.anim.name);

        float vfxStartTime = duration * skill.config.vfx.startAtProgress;
        yield return new WaitForSeconds(vfxStartTime);

        if (skill.config.vfx.particle != null)
        {
            var anchor = _player.skills.GetSkillAnchor(skill.config.vfx.anchorType);
            var particle = Instantiate(skill.config.vfx.particle);
            particle.transform.position = anchor.transform.position;
            particle.transform.forward = Vector3.Normalize(Camera.main.transform.position - anchor.transform.position);
            particle.Emit(1);
        }
    }*/
}