using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : Photon.MonoBehaviour {
  void Awake() {
    Movement      = new LobbyPlayerMovement();
    State         = new PlayerState(_colliderCenter, _colliderFoot);
    StateTransfer = new StateTransfer(_initState);
    SkillInfo     = new SkillInfo();
  }

  void FixedUpdate() {
    Movement.FixedUpdate(_rigid, _trans);
  }

  public PhotonView       PhotonView   { get { return _photonView;   } }
  public SpriteRenderer[] Renderers    { get { return _renderers;    } }
  public Rigidbody2D      Rigid        { get { return _rigid;        } }
  public BoxCollider2D    ColliderFoot { get { return _colliderFoot; } }

  public LobbyPlayerMovement Movement      { get; private set; }
  public PlayerState         State         { get; private set; }
  public StateTransfer       StateTransfer { get; private set; }
  public SkillInfo           SkillInfo     { get; private set; }

  [SerializeField] private PhotonView       _photonView;
  [SerializeField] private Transform        _trans;
  [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
  [SerializeField] private Rigidbody2D      _rigid;
  [SerializeField] private BoxCollider2D    _colliderCenter;
  [SerializeField] private BoxCollider2D    _colliderFoot;
  private static readonly string _initState = "Idle";
}

