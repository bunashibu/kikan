using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
  public void ChangeScene() {
    SceneManager.LoadScene(_sceneName);
  }

  [SerializeField] string _sceneName;
}
