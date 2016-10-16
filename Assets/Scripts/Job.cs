using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour {
  void Start() {
    _renderer = gameObject.GetComponent<SpriteRenderer>();
  }

  SpriteRenderer _renderer;
  [SerializeField] Sprite _actionNormal;
  [SerializeField] Sprite _actionX;
  [SerializeField] Sprite _actionShift;
}
