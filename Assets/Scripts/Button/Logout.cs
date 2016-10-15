using UnityEngine;
using System.Collections;

public class Logout : MonoBehaviour {
  public void ToLogin() {
    Application.LoadLevel("login");
  }
}
