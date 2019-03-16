using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : MonoBehaviour {
    void Awake() {
      _buttons[0].onClick.AddListener( () => Pick(0) );
      _buttons[1].onClick.AddListener( () => Pick(1) );
    }

    void Start() {
      EnableAllButtons();

      MonoUtility.Instance.DelaySec(_selectTime, () => {
        ActivatePlayer();

        Destroy(gameObject);
      });
    }

    public void Pick(int n) {
      _index = n;
      DisableAllButtons();
    }

    private void RandomPick() {
      int n = Random.Range(0, _jobs.Length);
      Pick(n);
    }

    private void ActivatePlayer() {
      if (_index == -1)
        RandomPick();

      _instantiator.InstantiatePlayer(_jobs[_index].name);
      _instantiator.InstantiateHudObjects(_canvas, _skillPanelList[_index]);
    }

    private void EnableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = true;
    }

    private void DisableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = false;
    }

    [SerializeField] private StartUpInstantiator _instantiator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private List<SkillPanel> _skillPanelList;
    private int _index = -1;

    [Header("Job Select Time (Second)")]
    [SerializeField] private float _selectTime;
  }
}

