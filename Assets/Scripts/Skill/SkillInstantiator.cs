using UnityEngine;
using System;
using System.Collections;

public class SkillInstantiator : Photon.MonoBehaviour {
  void Update() {
    if (photonView.isMine) {
      for (int i=0; i<_keys.Length; ++i) {
        if (_canUse && Input.GetKey(_keys[i])) {
          InstantiateSkill(i);
          UpdateCT(i);
          break;
        }
      }
    }
  }

  private void InstantiateSkill(int i) {
    string path = "Prehabs/Skill/" + _jobName + "/" + _names[i];

    var offset = _appearOffset[i];
    if (_renderer.flipX)
      offset.x *= -1;

    var skill = PhotonNetwork.Instantiate(path, this.transform.position + offset, Quaternion.identity, 0);

    FixFlipX(skill);

    var skillBehaviour = skill.GetComponent<Skill>();
    skillBehaviour.SetStatus(_status);
    Debug.Log("SetStatus");
  }

  private void FixFlipX(GameObject skill) {
    var skillSprite = skill.GetComponent<SpriteRenderer>();
    skillSprite.flipX = _renderer.flipX;
  }

  private void UpdateCT(int i) {
    _canUse = false;
    MonoUtility.Instance.DelaySec(_skillCT[i], () => {
      _canUse = true;
    });

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
  [SerializeField] private Vector3[] _appearOffset;
  [SerializeField] private Animator _weaponAnim;
  [SerializeField] private RigidState _rigidState;
  [SerializeField] private SkillInfo _skillInfo;
  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] private PlayerStatus _status;
  private bool _canUse = true;
}

