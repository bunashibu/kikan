using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour {
  public void MoveRight() {
    if (Input.GetKey(KeyCode.RightArrow)) {
      gameObject.transform.position += Vector3.right * 0.02f;
      canvas.transform.position += Vector3.right * 0.02f * 0.01f;
    }
  }

  public void MoveLeft() {
    if (Input.GetKey(KeyCode.LeftArrow)) {
      gameObject.transform.position += Vector3.left * 0.02f;
      canvas.transform.position += Vector3.left * 0.02f * 0.01f;
    }
  }

  public void Move() {
    MoveRight();
    MoveLeft();
  }

  void Update() {
    Move();
  }

  public Canvas canvas;
}
