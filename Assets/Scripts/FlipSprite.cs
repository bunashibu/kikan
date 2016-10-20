using UnityEngine;
using System.Collections;

public class FlipSprite : MonoBehaviour {
  public void Flip() {
    if (Input.GetKey(KeyCode.RightArrow))
      GetComponent<SpriteRenderer>().flipX = true;

    if (Input.GetKey(KeyCode.LeftArrow))
      GetComponent<SpriteRenderer>().flipX = false;
  }

  void Update() {
    Flip();
  }
}
