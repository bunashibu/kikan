using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  // PLAN: BattlePlayer will be merged with LobbyPlayer.
  //       and will be renamed to Player.
  [RequireComponent(typeof(BattlePlayerObserver))]
  public class BattlePlayer : MonoBehaviour, ICharacter, IBattle, IRewardTaker, ISpeaker {
    void Awake() {
      Assert.IsTrue(_killExpTable.Data.Count  == MaxLevel);
      Assert.IsTrue(_killGoldTable.Data.Count == MaxLevel);
      Assert.IsTrue(_hpTable.Data.Count       == MaxLevel);
      Assert.IsTrue(_expTable.Data.Count      == MaxLevel);

      _mediator     = new PlayerMediator(this);
      Teammates     = new List<IBattle>();
      Movement      = new BattlePlayerMovement(_core);
      State         = new CharacterState(_ladderCollider, _footCollider);
      BuffState     = new BuffState(Observer);
      Hp            = new PlayerHp();
      Exp           = new PlayerExp();
      Level         = new PlayerLevel();
      Gold          = new PlayerGold();
      KillDeath     = new PlayerKillDeath();
      Status        = new PlayerStatus(_jobStatus);
      StateTransfer = new StateTransfer(_initState, _animator);
      SkillInfo     = new SkillInfo();
      PlayerInfo    = new PlayerInfo(this);

      KillCount  = new ReactiveProperty<int>(0);
      DeathCount = new ReactiveProperty<int>(0);

      if (StageReference.Instance.StageData.Name == "Battle") {
        _mediator.AddListener(Hp.OnNotify);
        _mediator.AddListener(Exp.OnNotify);
        _mediator.AddListener(Level.OnNotify);
        _mediator.AddListener(Gold.OnNotify);

        // NOTE: Below environments exist in "Battle" global space
        _mediator.AddListener(NumberPopupEnvironment.Instance.OnNotify);
        _mediator.AddListener(KillRewardEnvironment.Instance.OnNotify);

        Level.AddListener(_mediator.OnNotify);

        if (PhotonView.isMine) {
          _worldHpBar.gameObject.SetActive(false);
          CameraInitializer.Instance.RegisterToTrackTarget(gameObject);
        }
        else {
          Hp.AddListener(_worldHpBar.OnNotify);
          _audioListener.enabled = false;
        }
      }
    }

    void Start() {
      PlayerInitializer.Instance.Initialize(this);
    }

    void FixedUpdate() {
      Movement.FixedUpdate(_rigid, transform);
    }

    public void OnNotify(Notification notification, object[] args) {
      _mediator.OnNotify(notification, args);
    }

    public PhotonView       PhotonView   { get { return _photonView;   } }
    public Transform        Transform    { get { return transform;     } }
    public SpriteRenderer[] Renderers    { get { return _renderers;    } }
    public Rigidbody2D      Rigid        { get { return _rigid;        } }
    public Collider2D       BodyCollider { get { return _bodyCollider; } }
    public Collider2D       FootCollider { get { return _footCollider; } }
    public Animator         Animator     { get { return _animator;     } }

    public BattlePlayerObserver Observer { get { return _observer; } }

    public AudioEnvironment     AudioEnvironment { get { return _audioEnvironment; } }

    public List<IBattle>        Teammates     { get; private set; }

    public BattlePlayerMovement Movement      { get; private set; }
    public CharacterState       State         { get; private set; }
    public BuffState            BuffState     { get; private set; }
    public PlayerHp             Hp            { get; private set; }
    public PlayerExp            Exp           { get; private set; }
    public PlayerLevel          Level         { get; private set; }
    public PlayerGold           Gold          { get; private set; }
    public PlayerKillDeath      KillDeath     { get; private set; }
    public PlayerStatus         Status        { get; private set; }
    public StateTransfer        StateTransfer { get; private set; }
    public SkillInfo            SkillInfo     { get; private set; }
    public PlayerInfo           PlayerInfo    { get; private set; }

    public int MaxLevel     => 15;
    public int MaxGold      => 99999;
    public int KillExp      => _killExpTable.Data[Level.Cur - 1];
    public int KillGold     => _killGoldTable.Data[Level.Cur - 1];
    public int DamageSkinId => 0;
    public int Power        { get { double ratio = (double)((Core.Attack + 100) / 100.0);
                                    return (int)(Status.Atk * Status.MulCorrectionAtk * ratio); } }
    public int Critical     => Core.Critical;

    public ReactiveProperty<int> KillCount  { get; private set; }
    public ReactiveProperty<int> DeathCount { get; private set; }

    public ReadOnlyCollection<int> HpTable  => _hpTable.Data;
    public ReadOnlyCollection<int> ExpTable => _expTable.Data;

    public NameBackground NameBackground { get { return _nameBackground; } }
    public PopupRemark    PopupRemark    { get { return _popupRemark;    } }

    public Character2D    Character { get { return _character; } }

    public PlayerCore      Core      { get { return _core;      } }

    public DamageSkin      DamageSkin { get { return _damageSkin; } }

    public Weapon          Weapon { get { return _weapon; } }

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView       _photonView;
    [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
    [SerializeField] private Rigidbody2D      _rigid;
    [SerializeField] private Collider2D       _bodyCollider;
    [SerializeField] private Collider2D       _ladderCollider;
    [SerializeField] private Collider2D       _footCollider;
    [SerializeField] private Animator         _animator;
    [SerializeField] private AudioListener    _audioListener;

    [Header("Observer")]
    [SerializeField] private BattlePlayerObserver _observer;

    [Header("Environment")]
    [SerializeField] private AudioEnvironment _audioEnvironment;

    [Header("Data")]
    [SerializeField] private DataTable _hpTable;
    [SerializeField] private DataTable _expTable;
    [SerializeField] private DataTable _killExpTable;
    [SerializeField] private DataTable _killGoldTable;

    [SerializeField] private Bar _worldHpBar;

    // Obsolete
    [Header("Sync On Their Own")]
    [SerializeField] private PlayerCore _core;

    [Header("Canvas")]
    [SerializeField] private NameBackground _nameBackground;
    [SerializeField] private PopupRemark    _popupRemark;

    [Space(10)]
    [SerializeField] private Character2D _character;

    [Space(10)]
    [SerializeField] private DamageSkin _damageSkin;

    [Space(10)]
    [SerializeField] private JobStatus _jobStatus;
    [SerializeField] private Weapon    _weapon;

    private static readonly string _initState = "Idle";
    private PlayerMediator _mediator;
  }
}

