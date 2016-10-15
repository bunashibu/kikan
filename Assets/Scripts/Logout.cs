using UnityEngine;
using System.Collections;

public class Logout : MonoBehaviour {
  public void toLogin() {
    Application.LoadLevel("login");
  }
}
