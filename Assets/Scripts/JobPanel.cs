using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : Photon.PunBehaviour {
    void Start() {
      EnableAllButtons();

      MonoUtility.Instance.DelaySec(10.0f, () => {
        ActivatePlayer();

        Destroy(gameObject);
        Destroy(_playerInstantiator);
      });
    }

    public void Pick(int n) {
      _index = n;
      DisableAllButtons();
    }

    private void RandomPick() {
      int n = (int)(Random.value * (_jobs.Length - 1));
      Pick(n);
    }

    private void ActivatePlayer() {
      if (_index == -1)
        RandomPick();

      _playerInstantiator.InstantiatePlayer(_jobs[_index].name);
      _playerInstantiator.InstantiateHudObjects(_canvas, _skillPanelList[_index]);
      _playerInstantiator.InitAll(_jobStatus[_index]);
    }

    private void EnableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = true;
    }

    private void DisableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = false;
    }

    [SerializeField] private PlayerInstantiator _playerInstantiator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private JobStatus[] _jobStatus;
    [SerializeField] private List<SkillPanel> _skillPanelList;
    private int _index = -1;
  }
}

