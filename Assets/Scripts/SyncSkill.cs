using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncSkill : MonoBehaviour {
  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if (stream.isWriting) {
      stream.SendNext(_renderer.flipX);
      /*
      stream.SendNext(_skill.status.lv);
      stream.SendNext(_skill.status.atk);
      stream.SendNext(_skill.status.dfn);
      stream.SendNext(_skill.status.spd);
      stream.SendNext(_skill.status.jmp);
      */
    } else {
      _renderer.flipX = (bool)stream.ReceiveNext();
      /*
      _skill.status.lv = (int)stream.ReceiveNext();
      _skill.status.atk = (int)stream.ReceiveNext();
      _skill.status.dfn = (int)stream.ReceiveNext();
      _skill.status.spd = (int)stream.ReceiveNext();
      _skill.status.jmp = (int)stream.ReceiveNext();
      */
    }
  }

  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] private Skill _skill;
}

