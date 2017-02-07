using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
  public void ChangeScene(string nextSceneName) {
    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

    Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
    MonoUtility.Instance.DelayUntil(() => nextScene.isLoaded, () => {
      SceneManager.SetActiveScene(nextScene);
    });
  }
}
