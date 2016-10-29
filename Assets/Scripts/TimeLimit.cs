using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeLimit : MonoBehaviour {
  void Start() {
    GetComponent<Text>().text = ((int)time).ToString();
  }

  void Update() {
    time -= Time.deltaTime;
    if (time < 0)
      time = 0;
    GetComponent<Text>().text = ((int)time).ToString();
  }

  private float time = 60;
}
