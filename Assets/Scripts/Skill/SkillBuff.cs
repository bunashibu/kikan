using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class SkillBuff : Skill {
    void Awake() {
      _renderer = GetComponent<SpriteRenderer>();
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Where(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          return Client.Opponents.Contains(skillUser);
        })
        .Subscribe(_ => _renderer.color = _opponentColor);
    }

    void Update() {
      transform.position = _skillUserObj.transform.position + _offset;
    }

    void OnDestroy() {
      if (photonView.isMine)
        SkillReference.Instance.Remove(viewID);
    }

    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _opponentColor;
    private SpriteRenderer _renderer;
  }
}
