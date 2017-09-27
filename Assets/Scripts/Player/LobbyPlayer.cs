using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LobbyPlayer : Photon.MonoBehaviour, ICharacter {
    void Awake() {
      Movement      = new LobbyPlayerMovement();
      State         = new PlayerState(_collider, _collider);
      StateTransfer = new StateTransfer(_initState, _animator);
      SkillInfo     = new SkillInfo();
  
      Movement.SetLinearMoveForce(80.0f);
      Movement.SetJumpForce(400.0f);
    }
  
    void FixedUpdate() {
      if (State.Ground) {
        Rigid.AddForce(Physics2D.gravity * -2.4f);
      }
  
      Movement.FixedUpdate(_rigid, _trans);
    }
  
    public PhotonView       PhotonView { get { return _photonView;   } }
    public SpriteRenderer[] Renderers  { get { return _renderers;    } }
    public Rigidbody2D      Rigid      { get { return _rigid;        } }
    public BoxCollider2D    Collider   { get { return _collider; } }
  
    public LobbyPlayerMovement Movement      { get; private set; }
    public PlayerState         State         { get; private set; }
    public StateTransfer       StateTransfer { get; private set; }
    public SkillInfo           SkillInfo     { get; private set; }
  
    [SerializeField] private PhotonView       _photonView;
    [SerializeField] private Transform        _trans;
    [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
    [SerializeField] private Rigidbody2D      _rigid;
    [SerializeField] private BoxCollider2D    _collider;
    [SerializeField] private Animator         _animator;
    private static readonly string _initState = "Idle";
  }
}

