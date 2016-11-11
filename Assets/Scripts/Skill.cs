using UnityEngine;
using System;
using System.Collections;

public class Skill : MonoBehaviour {
  protected IEnumerator DelayMethod(float waitTime, Action action) {
    yield return new WaitForSeconds(waitTime);
    action();
  }

  public void SkillX() {
    if (Input.GetKey(KeyCode.X))
      _anim.SetBool("SkillX", true);
    else
      _anim.SetBool("SkillX", false);
  }

  public void SkillShift() {
    if (Input.GetKey(KeyCode.LeftShift))
      _anim.SetBool("SkillShift", true);
    else
      _anim.SetBool("SkillShift", false);
    /*
    GetComponent<SpriteRenderer>().sprite = _actionShift;
    StartCoroutine(DelayMethod(0.5f, () => {
      GetComponent<SpriteRenderer>().sprite = _actionNormal;
    }));
    */
  }

  void Update() {
    SkillX();
    SkillShift();
  }

  [SerializeField] private Animator _anim;
}
