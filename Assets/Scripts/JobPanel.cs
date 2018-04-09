using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : MonoBehaviour {
    void Start() {
      //Destroy(gameObject, 10.0f);
    }

    public void Pick(int n) {
      _playerInstantiator.InstantiatePlayer(_jobs[n].name);
      _playerInstantiator.InstantiateHudObjects(_canvas, _skillPanelList[n]);
      _playerInstantiator.InitAll(_jobStatus[n]);

      DisableAllButtons();
      Destroy(gameObject);
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

  }
}

