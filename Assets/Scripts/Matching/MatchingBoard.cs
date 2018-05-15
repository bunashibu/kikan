using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class MatchingBoard : MonoBehaviour {
    void Start() {
      _isApplying = false;
      _apply.SetActive(true);
      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    [SerializeField] private GameObject _apply;
    [SerializeField] private GameObject _cancel;
    [SerializeField] private GameObject _nameBoard;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _startPanel;
    private bool _isApplying;
  }
}

