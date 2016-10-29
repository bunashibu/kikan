using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour {
  public void MoveRight() {
    if (Input.GetKey(KeyCode.RightArrow)) {
      GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
      GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));
    }
  }

  public void MoveLeft() {
    if (Input.GetKey(KeyCode.LeftArrow)) {
      GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
      GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0));
    }
  }

  public void Move() {
    MoveRight();
    MoveLeft();
  }

  public void Jump() {
    if (Input.GetButtonDown("Jump")) {
      GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
    }
  }

  void Update() {
    Move();
    Jump();
  }

  public Canvas canvas;
}
