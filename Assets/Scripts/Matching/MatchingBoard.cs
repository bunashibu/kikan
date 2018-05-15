using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class MatchingBoard : MonoBehaviour {
    void Start() {
      SetApplyMode();
    }

    public void SetApplyMode() {
      _apply.SetActive(true);

      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    public void SetMatchWaitMode() {
      _cancel.SetActive(true);
      _nameBoard.SetActive(true);
      _progressLabel.SetActive(true);

      _apply.SetActive(false);
    }

    [SerializeField] private GameObject _apply;
    [SerializeField] private GameObject _cancel;
    [SerializeField] private GameObject _nameBoard;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _startPanel;
  }
}

