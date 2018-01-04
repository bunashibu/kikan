using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerPortal : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
      GameObject target = collider.gameObject;

      if (target.layer == LayerMask.NameToLayer(_portalLayerName)) {
        if (Input.GetKey(KeyCode.UpArrow)) {
          target.GetComponent<Portal>().Enter(_player);
        }
      }
    }

    [SerializeField] private string _portalLayerName;
    [SerializeField] private BattlePlayer _player;
  }
}

