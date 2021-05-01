﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Bunashibu.Kikan {
  public class SceneChanger : SingletonMonoBehaviour<SceneChanger> {
    protected override void Awake() {
      _alphaMask = new Color(0, 0, 0, 0.075f);
    }

    void Start() {
      _shouldFadeIn = true;
    }

    void Update() {
      if (_shouldFadeIn)
        FadeIn();

      if (_shouldFadeOut)
        FadeOut();
    }

    public void ChangeScene(string nextSceneName) {
      PhotonNetwork.isMessageQueueRunning = false;
      _shouldFadeOut = true;

      MonoUtility.Instance.DelayUntil(() => _shouldFadeOut == false, () => {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
      });
    }

    public void ChangeSceneWithSE(string nextSceneName, AudioSource source, AudioClip clip, float volume = 1.0f) {
      PhotonNetwork.isMessageQueueRunning = false;
      _shouldFadeOut = true;
      source.PlayOneShot(clip, volume);

      MonoUtility.Instance.DelayUntil(() => (_shouldFadeOut == false) && !source.isPlaying, () => {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
      });
    }

    public void FadeOutAndLeaveRoom() {
      _shouldFadeOut = true;
      PhotonNetwork.LeaveRoom();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
      PhotonNetwork.isMessageQueueRunning = true;
    }

    private void FadeIn() {
      _fadeImage.color = _fadeImage.color - _alphaMask;

      if (_fadeImage.color.a <= 0)
        _shouldFadeIn = false;
    }

    private void FadeOut() {
      _fadeImage.color = _fadeImage.color + _alphaMask;

      if (_fadeImage.color.a >= 1)
        _shouldFadeOut = false;
    }

    [SerializeField] private Image _fadeImage;
    private Color _alphaMask;
    private bool _shouldFadeIn;
    private bool _shouldFadeOut;
  }
}
