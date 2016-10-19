using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
  public void MoveRight() {
    if (Input.GetKey(KeyCode.RightArrow)) {
      gameObject.transform.position += Vector3.right * 0.02f;
      GetComponent<SpriteRenderer>().flipX = true;
    }
  }

  public void MoveLeft() {
    if (Input.GetKey(KeyCode.LeftArrow)) {
      gameObject.transform.position += Vector3.left * 0.02f;
      GetComponent<SpriteRenderer>().flipX = false;
    }
  }

  public void Move() {
    MoveRight();
    MoveLeft();
  }

  void Update() {
    Move();
  }
}
