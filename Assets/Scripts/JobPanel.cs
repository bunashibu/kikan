using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : Photon.PunBehaviour {
    void Start() {
      EnableAllButtons();

      MonoUtility.Instance.DelaySec(10.0f, () => {
        if (!_isPicked)
          RandomPick();

        Destroy(gameObject);
        Destroy(_playerInstantiator.gameObject);
      });
    }

    public void Pick(int n) {
      _isPicked = true;

      _playerInstantiator.InstantiatePlayer(_jobs[n].name);
      _playerInstantiator.InstantiateHudObjects(_canvas, _skillPanelList[n]);
      _playerInstantiator.InitAll(_jobStatus[n]);

      DisableAllButtons();
    }

    private void RandomPick() {
      int n = (int)(Random.value * (_jobs.Length - 1));
      Pick(n);
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
    private bool _isPicked;
  }
}

