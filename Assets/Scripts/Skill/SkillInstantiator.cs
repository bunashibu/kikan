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

    Debug.Log(_names[i]);

    PhotonNetwork.Instantiate(path, pos, Quaternion.identity, 0);

    _canUse = false;
    MonoUtility.Instance.DelaySec(_skillCT[i], () => {
      _canUse = true;
    });

    /*
    _weaponAnim.SetBool(_names[i], true);
    MonoUtility.Instance.DelayOneFrame(() => {
      _weaponAnim.SetBool(_names[i], false);
    });
    */

    _skillInfo.SetState(_names[i], SkillState.Using);
    MonoUtility.Instance.DelaySec(_skillCT[i], () => {
      _skillInfo.SetState(_names[i], SkillState.Ready);
    });

    _rigidState.Rigor = true;
    MonoUtility.Instance.DelaySec(_rigorCT[i], () => {
      _rigidState.Rigor = false;
      _skillInfo.SetState(_names[i], SkillState.Used);
    });
  }

  [SerializeField] private string _jobName;
  [SerializeField] private KeyCode[] _keys;
  [SerializeField] private SkillName[] _names;
  [SerializeField] private float[] _skillCT;
  [SerializeField] private float[] _rigorCT;
  [SerializeField] private Animator _weaponAnim;
  [SerializeField] private RigidState _rigidState;
  [SerializeField] private SkillInfo _skillInfo;
  private bool _canUse = true;
}

