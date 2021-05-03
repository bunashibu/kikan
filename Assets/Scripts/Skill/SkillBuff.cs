using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillBuff : Skill {
    void Update() {
      transform.position = _skillUserObj.transform.position + _offset;
    }

    void OnDestroy() {
      if (photonView.isMine)
        SkillReference.Instance.Remove(viewID);
    }

    [SerializeField] private Vector3 _offset;
  }
}
