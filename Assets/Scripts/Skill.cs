using UnityEngine;
using System;
using System.Collections;

public class Skill : MonoBehaviour {
  void Update() {
    UpdateFlag();
  }

  public void Activate() {
    if (!_flag) return;

    _anim.SetBool(_name, true);
    StartCoroutine(DelayOneFrame(() => {
      _anim.SetBool(_name, false);
    }));

    StartCoroutine(DelaySec(_ct, () => {
      canUse = true;
    }));

    canUse = false;
    _flag = false;
  }

  private void UpdateFlag() {
    if (canUse && Input.GetKey(_key))
      _flag = true;
  }

  private IEnumerator DelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  private IEnumerator DelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }

  [SerializeField] private KeyCode _key;
  [SerializeField] private Animator _anim;
  [SerializeField] private string _name;
  [SerializeField] private float _ct;
  [NonSerialized] public bool canUse = true;
  private bool _flag;
}

