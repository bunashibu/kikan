using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour {
  void Awake() {
    _states = new Dictionary<SkillName, SkillState>();

    _states.Add(SkillName.X, SkillState.Ready);
    _states.Add(SkillName.Shift, SkillState.Ready);
    _states.Add(SkillName.Z, SkillState.Ready);
  }

  public SkillState GetState(SkillName name) {
    return _states[name];
  }

  public void SetState(SkillName name, SkillState state) {
    _states[name] = state;
  }

  private Dictionary<SkillName, SkillState> _states;
}

