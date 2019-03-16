using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : MonoBehaviour {
    void Awake() {
      _buttons[0].onClick.AddListener( () => Pick(0) );
      _buttons[1].onClick.AddListener( () => Pick(1) );
      _decideButton.onClick.AddListener( () => Decide() );
    }

    void Start() {
      EnableAllButtons();

      MonoUtility.Instance.DelaySec(_selectTime, () => {
        ActivatePlayer();

        Destroy(gameObject);
      });
    }

    public void Pick(int n) {
      if (_curPick == -1) {
        _curPick = n;
        _buttons[_curPick].interactable = false;
      }
      else {
        _prePick = _curPick;
        _curPick = n;
        _buttons[_prePick].interactable = true;
        _buttons[_curPick].interactable = false;
      }

      _description.UpdateLabel(n);
    }

    private void RandomPick() {
      int n = Random.Range(0, _jobs.Length);
      Pick(n);
    }

    private void Decide() {
      _decideButton.interactable = false;
      DisableAllButtons();
    }

    private void ActivatePlayer() {
      if (_decideButton.interactable)
        RandomPick();

      _instantiator.InstantiatePlayer(_jobs[_curPick].name);
      _instantiator.InstantiateHudObjects(_canvas, _skillPanelList[_curPick]);
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
    [SerializeField] private Button _decideButton;
    [SerializeField] private JobDescription _description;
    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private Button[] _buttons;

    // In order to avoid setting wrong index on inspector,
    // _skillPanelList is here. not in _instantiator.
    [SerializeField] private List<SkillPanel> _skillPanelList;

    [Header("Job Select Time (Second)")]
    [SerializeField] private float _selectTime;

    private int _prePick = -1;
    private int _curPick = -1;
  }
}

