using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleApplication : MonoBehaviour {
  public void ToBattle() {
    SceneManager.LoadScene("battle");
  }
}
