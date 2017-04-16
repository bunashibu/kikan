using UnityEngine;
using System;
using System.Collections;

public class SkillInstantiator : Photon.MonoBehaviour {
  void Update() {
    if (photonView.isMine) {
      for (int i=0; i<_keys.Length; ++i) {
        if (_canUse && Input.GetKey(_keys[i])) {
          InstantiateSkill(i);
          break;
        }
      }
    }
  }

  private void InstantiateSkill(int i) {
    string path = "Prehabs/Skill/" + _jobName + "/" + _names[i];
    var pos = this.transform.position + new Vector3(-0.4f, 0.1f, 0);

    PhotonNetwork.Instantiate(path, pos, Quaternion.identity, 0);

    _canUse = false;
    MonoUtility.Instance.DelaySec(_skillCT[i], () => {
      _canUse = true;
    });

    _rigidState.UsingSkill = true;
    MonoUtility.Instance.DelaySec(_immobileCT[i], () => {
      _rigidState.UsingSkill = false;
    });

    _weaponAnim.SetBool(_names[i], true);
    MonoUtility.Instance.DelayOneFrame(() => {
      _weaponAnim.SetBool(_names[i], false);
    });
  }

  [SerializeField] private string _jobName;
  [SerializeField] private KeyCode[] _keys;
  [SerializeField] private string[] _names;
  [SerializeField] private float[] _skillCT;
  [SerializeField] private float[] _immobileCT;
  [SerializeField] private Animator _weaponAnim;
  [SerializeField] private RigidState _rigidState;
  private bool _canUse = true;
}

