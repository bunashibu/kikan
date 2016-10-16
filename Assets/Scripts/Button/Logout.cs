using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Logout : MonoBehaviour {
  public void ToLogin() {
    SceneManager.LoadScene("login");
  }
}
