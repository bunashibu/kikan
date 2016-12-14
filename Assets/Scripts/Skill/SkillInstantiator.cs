using UnityEngine;
using System;
using System.Collections;

public class SkillInstantiator : MonoBehaviour {
  void Update() {
    for (int i=0; i<_keys.Length; ++i) {
      if (_canUse && Input.GetKey(_keys[i])) {
        InstantiateSkill(i);
        break;
      }
    }
  }

  private void InstantiateSkill(int i) {
    Instantiate(_skills[i], this.transform.position + new Vector3(-0.4f, -0.1f, 0), this.transform.rotation);

    _canUse = false;
    StartCoroutine(MonoUtility.Instance.DelaySec(_ct[i], () => {
      _canUse = true;
    }));

    _anim.SetBool(_names[i], true);
    StartCoroutine(MonoUtility.Instance.DelayOneFrame(() => {
      _anim.SetBool(_names[i], false);
    }));
  }

  [SerializeField] private Skill[] _skills;
  [SerializeField] private KeyCode[] _keys;
  [SerializeField] private string[] _names;
  [SerializeField] private float[] _ct;
  [SerializeField] private Animator _anim;
  private bool _canUse = true;
}

