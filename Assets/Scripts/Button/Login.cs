using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour {
  public void ToLobby() {
    SceneManager.LoadScene("lobby");
  }
}
