using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class LoadFirstScene : SingletonMonoBehaviour<LoadFirstScene> {
  void Start() {
    if (SceneManager.sceneCount == 1)
      SceneManager.LoadSceneAsync(_firstSceneName, LoadSceneMode.Additive);

    Scene firstScene = SceneManager.GetSceneByName(_firstSceneName);
    MonoUtility.Instance.DelayUntil(() => firstScene.isLoaded, () => {
      SceneManager.SetActiveScene(firstScene);
    });
  }

  [SerializeField] private string _firstSceneName;
}

