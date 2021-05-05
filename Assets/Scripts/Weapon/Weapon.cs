using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Weapon : MonoBehaviour {
    protected void Awake() {
      Stream = new WeaponStream();
      _canInstantiate = new ReactiveProperty<bool>(true);

      _instantiator = new SkillInstantiator(this, _player);
      _ctManager = new SkillCTManager(this, _player);

      _player.Level.Cur
        .Subscribe(level => {
          if (level == 3)
            Stream.OnNextSkillAvailable(1);
          else if (level == 5)
            Stream.OnNextSkillAvailable(2);
          else if (level == 7)
            Stream.OnNextSkillAvailable(3);
          else if (level == 8)
            Stream.OnNextSkillAvailable(4);
        });

      this.UpdateAsObservable()
        .Take(1)
        .Where(_ => {
          return Client.Opponents.Contains(_player);
        })
        .Subscribe(_ => _renderer.sprite = _opponentSprite);
    }

    public virtual bool IsUsable(int i) {
      return _ctManager.IsUsable(i);
    }

    public bool IsRequiredLv(int i) {
      return _player.Level.Cur.Value >= _requireLv[i];
    }

    public void EnableInstantiate() {
      _canInstantiate.Value = true;
    }

    public void DisableInstantiate() {
      _canInstantiate.Value = false;
    }

    public virtual void ResetAllCT() {
      _ctManager.ResetAllCT();
    }

    public WeaponStream Stream { get; private set; }

    public IReadOnlyReactiveProperty<bool> CanInstantiate => _canInstantiate;
    private ReactiveProperty<bool> _canInstantiate;

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
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _opponentSprite;

    protected SkillInstantiator _instantiator;
    protected SkillCTManager _ctManager;
  }

  [System.SerializableAttribute]
  public class KeyList {
    public List<KeyCode> keys;
  }
}
