using UnityEngine;
using System;
using System.Collections;

public class Job : MonoBehaviour {
  protected IEnumerator DelayMethod(float waitTime, Action action) {
    yield return new WaitForSeconds(waitTime);
    action();
  }

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

  public void DecreaseLife(int damage) {
    _life -= damage;
    if (_life < 0)
      _life = 0;
  }

  public int GetLife() {
    return _life;
  }

  public int GetMaxLife() {
    return _maxLife;
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

  [SerializeField] private Sprite _actionNormal;
  [SerializeField] private Sprite _actionX;
  [SerializeField] private Sprite _actionShift;
  [SerializeField] private int _life;
  [SerializeField] private int _maxLife;
}
