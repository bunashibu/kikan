using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  public class LobbyPlayer : Photon.MonoBehaviour, ICharacter {
    void Awake() {
      Movement      = new LobbyPlayerMovement();
      State         = new PlayerState(_collider, _collider);
      StateTransfer = new StateTransfer(_initState, _animator);
      SkillInfo     = new SkillInfo();

      Movement.SetMoveForce(4.0f);
      Movement.SetJumpForce(400.0f);
    }

    void FixedUpdate() {
      Movement.FixedUpdate(_rigid, _trans);
    }

    public Character2D      Character      { get { return _character;      }  }
    public PhotonView       PhotonView     { get { return _photonView;     }  }
    public SpriteRenderer[] Renderers      { get { return _renderers;      }  }
    public Rigidbody2D      Rigid          { get { return _rigid;          }  }
    public Collider2D       Collider       { get { return _collider;       }  }
    public Collider2D       LadderCollider { get { return _ladderCollider; }  }
    public Collider2D       FootCollider   { get { return _footCollider;   }  }

    public LobbyPlayerMovement Movement      { get; private set; }
    public PlayerState         State         { get; private set; }
    public StateTransfer       StateTransfer { get; private set; }
    public SkillInfo           SkillInfo     { get; private set; }

    [SerializeField] private Character2D      _character;
    [SerializeField] private PhotonView       _photonView;
    [SerializeField] private Transform        _trans;
    [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
    [SerializeField] private Rigidbody2D      _rigid;
    [SerializeField] private Collider2D       _collider;
    [SerializeField] private Collider2D       _ladderCollider;
    [SerializeField] private Collider2D       _footCollider;
    [SerializeField] private Animator         _animator;
    private static readonly string _initState = "Idle";
  }
}

