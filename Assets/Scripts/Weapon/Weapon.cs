using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Weapon : MonoBehaviour {
    protected void Awake() {
      Stream = new WeaponStream();
      CanInstantiate = true;

      _instantiator = new SkillInstantiator(this, _player);
      _ctManager = new SkillCTManager(this, _player);
    }

    public virtual bool IsUsable(int i) {
      return _ctManager.IsUsable(i);
    }

    public bool IsReachedToRequireLv(int i) {
      return _player.Level.Cur.Value >= _requireLv[i];
    }

    public void EnableInstantiate() {
      CanInstantiate = true;
    }

    public void DisableInstantiate() {
      CanInstantiate = false;
    }

    public virtual void ResetAllCT() {
      _ctManager.ResetAllCT();
    }

    public WeaponStream Stream { get; private set; }
    public bool CanInstantiate { get; private set; }

    public string        JobName      => _jobName;
    public List<KeyList> KeysList     => _keysList;
    public SkillName[]   SkillNames   => _skillNames;
    public int[]         RequireLv    => _requireLv;
    public Vector3[]     AppearOffset => _appearOffset;
    public float[]       SkillCT      => _skillCT;
    public float[]       RigorCT      => _rigorCT;

    // MEMO: All parameters should be composed like a skill slot.
    [SerializeField] protected Player _player;
    [SerializeField] private string _jobName;
    [SerializeField] private List<KeyList> _keysList;
    [SerializeField] protected SkillName[] _skillNames;
    [SerializeField] private int[] _requireLv;
    [SerializeField] private Vector3[] _appearOffset;
    [SerializeField] protected float[] _skillCT;
    [SerializeField] private float[] _rigorCT;

    protected SkillInstantiator _instantiator;
    protected SkillCTManager _ctManager;
  }

  [System.SerializableAttribute]
  public class KeyList {
    public List<KeyCode> keys;
  }
}

