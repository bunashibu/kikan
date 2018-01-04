using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Portal : MonoBehaviour {
    public void Enter(BattlePlayer target) {
      target.transform.position = _exit.transform.position;
      target.Rigid.velocity = new Vector2(0, 0);
    }

    [SerializeField] private GameObject _exit;
  }
}

