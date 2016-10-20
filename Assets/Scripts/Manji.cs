using UnityEngine;
using System;
using System.Collections;

public class Manji : Job {
  public void SkillX() {
    GetComponent<SpriteRenderer>().sprite = _actionX;
    StartCoroutine(DelayMethod(0.2f, () => {
      GetComponent<SpriteRenderer>().sprite = _actionNormal;
    }));
  }

  public void SkillShift() {
    GetComponent<SpriteRenderer>().sprite = _actionShift;
    StartCoroutine(DelayMethod(0.5f, () => {
      GetComponent<SpriteRenderer>().sprite = _actionNormal;
    }));
  }

  void Start() {
    GetComponent<SpriteRenderer>().sprite = _actionNormal;
  }

  void Update() {
    if (Input.GetKey(KeyCode.X))
      SkillX();

    if (Input.GetKey(KeyCode.LeftShift))
      SkillShift();
  }
}
