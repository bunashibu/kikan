using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class Ladder : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
      _target = collider.gameObject;

      if (_target.tag != "Player")
        return;

      _anim = _target.GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collider) {
      if (collider.gameObject.tag != "Player")
        return;
      if (_target != collider.gameObject)
        return;

      if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Ladder")) {
        _target.transform.position = new Vector3(gameObject.transform.position.x, _target.transform.position.y, 0);
      }
    }

    private GameObject _target;
    private Animator _anim;
  }
}

