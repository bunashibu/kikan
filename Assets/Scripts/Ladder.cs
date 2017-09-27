using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class Ladder : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
      _target = collider.gameObject;
      _anim = _target.GetComponent<Animator>();
    }
  
    void OnTriggerStay2D(Collider2D collider) {
      if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Climb")) {
        _target.transform.position = new Vector3(gameObject.transform.position.x, _target.transform.position.y, 0);
      }
    }
  
    private GameObject _target;
    private Animator _anim;
  }
}

