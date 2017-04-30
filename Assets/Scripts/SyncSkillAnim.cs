using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncSkillAnim : MonoBehaviour {
  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if (stream.isWriting) {
      stream.SendNext(_renderer.flipX);
    } else {
      _renderer.flipX = (bool)stream.ReceiveNext();
    }
  }

  [SerializeField] private SpriteRenderer _renderer;
}

