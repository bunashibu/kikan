using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(BattlePlayerObserver))]
  public class BattlePlayer : MonoBehaviour, IKillReward {
    void Awake() {
      Movement      = new BattlePlayerMovement();
      State         = new PlayerState(_ladderCollider, _footCollider);
      Hp            = new PlayerHp(this, _hpTable, _worldHpBar);
      StateTransfer = new StateTransfer(_initState, _animator);
      SkillInfo     = new SkillInfo();
    }

    void Update() {
      Vector2 footRayOrigin = new Vector2(_footCollider.bounds.center.x, _footCollider.bounds.min.y);

      float rayLength = 0.1f + Mathf.Abs(Rigid.velocity.y) * Time.deltaTime;
      RaycastHit2D hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down , rayLength, _groundMask);
      State.InGround = Mathf.Approximately(hitGround.distance, 0);

      if (State.Ground) {
        float angle = Vector2.Angle(hitGround.normal, Vector2.up);
        State.GroundAngle = angle;
        Debug.Log(angle);
      }

      Debug.DrawRay(footRayOrigin, Vector2.down * rayLength, Color.red);
    }

    [SerializeField] private LayerMask _groundMask;

    void OnCollisionEnter2D(Collision2D collision) {
      string layerName = LayerMask.LayerToName(collision.gameObject.layer);
      if (layerName == "Ground" || layerName == "CanNotDownGround") {
        Debug.Log("Collision Enter");
        State.Ground = true;
      }
    }

    void OnCollisionExit2D(Collision2D collision) {
      string layerName = LayerMask.LayerToName(collision.gameObject.layer);
      if (layerName == "Ground" || layerName == "CanNotDownGround") {
        Debug.Log("Collision Exit");
        State.Ground = false;
      }
    }

    void OnTriggerEnter2D(Collider2D collider) {
      string layerName = LayerMask.LayerToName(collider.gameObject.layer);
      if (layerName == "Ground" || layerName == "CanNotDownGround") {
        Debug.Log("Trigger Enter");
        State.Ground = true;
      }
    }

    void OnTriggerExit2D(Collider2D collider) {
      string layerName = LayerMask.LayerToName(collider.gameObject.layer);
      if (layerName == "Ground" || layerName == "CanNotDownGround") {
        Debug.Log("Trigger Exit");
        State.Ground = false;
      }
    }

    void FixedUpdate() {
      Movement.FixedUpdate(_rigid, _trans);
    }

    public PhotonView       PhotonView   { get { return _photonView;   } }
    public SpriteRenderer[] Renderers    { get { return _renderers;    } }
    public Rigidbody2D      Rigid        { get { return _rigid;        } }
    public Collider2D       FootCollider { get { return _footCollider; } }

    public BattlePlayerObserver Observer { get { return _observer; } }

    public BattlePlayerMovement Movement      { get; private set; }
    public PlayerState          State         { get; private set; }
    public PlayerHp             Hp            { get; private set; }
    public StateTransfer        StateTransfer { get; private set; }
    public SkillInfo            SkillInfo     { get; private set; }

    public int KillExp  { get { return _killExpTable.Data[Level.Lv - 1];  } }
    public int KillGold { get { return _killGoldTable.Data[Level.Lv - 1]; } }

    //
    // Consider
    //
    public PlayerNextExp   NextExp   { get { return _nextExp;   } }
    public PlayerLevel     Level     { get { return _level;     } }
    public PlayerGold      Gold      { get { return _gold;      } }
    public PlayerKillDeath KillDeath { get { return _killDeath; } }
    public PlayerCore      Core      { get { return _core;      } }

    public PlayerStatus     Status     { get { return _status;     } }
    public PlayerDamageSkin DamageSkin { get { return _damageSkin; } }

    // portal
    // respawner
    // automatic healer

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView       _photonView;
    [SerializeField] private Transform        _trans;
    [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
    [SerializeField] private Rigidbody2D      _rigid;
    [SerializeField] private Collider2D    _ladderCollider;
    [SerializeField] private Collider2D    _footCollider;
    [SerializeField] private Animator         _animator;

    [Header("Observer")]
    [SerializeField] private BattlePlayerObserver _observer;

    [Header("Data")]
    [SerializeField] private DataTable _hpTable;
    [SerializeField] private Bar       _worldHpBar;

    // Consider
    [Header("Sync On Their Own")]
    [SerializeField] private PlayerNextExp   _nextExp;
    [SerializeField] private PlayerLevel     _level;
    [SerializeField] private PlayerGold      _gold;
    [SerializeField] private PlayerKillDeath _killDeath;
    [SerializeField] private PlayerCore      _core;

    [Header("Kill Reward")]
    [SerializeField] private DataTable _killExpTable;
    [SerializeField] private DataTable _killGoldTable;

    [Space(10)]
    [SerializeField] private PlayerStatus     _status;
    [SerializeField] private PlayerDamageSkin _damageSkin;

    private static readonly string _initState = "Idle";
  }
}

