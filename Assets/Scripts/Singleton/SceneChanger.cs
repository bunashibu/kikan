using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
  public void ChangeScene(string nextSceneName) {
    PhotonNetwork.isMessageQueueRunning = false;

    SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

    Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
    MonoUtility.Instance.DelayUntil(() => nextScene.isLoaded, () => {
      Debug.Log(nextScene.name + " was loaded");
      SceneManager.SetActiveScene(nextScene);
      PhotonNetwork.isMessageQueueRunning = true;
    });
  }
}
