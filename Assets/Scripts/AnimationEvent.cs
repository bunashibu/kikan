using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class AnimationEvent : MonoBehaviour {
    public void OnAnimationFinishDestroy() {
      if (PhotonNetwork.player.IsMasterClient) {
        MonoUtility.Instance.DelaySec(2.0f, () => {
          PhotonNetwork.Destroy(gameObject);
        });
      }
    }
  }
}

