using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerPortal : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
      GameObject target = collider.gameObject;

      if (target.layer == LayerMask.NameToLayer(_portalLayerName)) {
        if (Input.GetKey(KeyCode.UpArrow)) {
          target.GetComponent<Portal>().Enter(gameObject);
        }
      }
    }

    [SerializeField] private string _portalLayerName;
  }
}

