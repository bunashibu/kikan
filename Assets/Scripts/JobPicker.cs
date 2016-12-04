using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    _player = Instantiate(_player) as GameObject;

    var job = Instantiate(_jobs[n]) as GameObject;
    job.transform.SetParent(_player.transform, false);

    var health = ScriptableObject.CreateInstance<Health>();
    //health.init(life, max);

    var hs = _player.GetComponent<HealthSystem>();
    hs.Init(health, manager);
    hs.Show();

    DisableAllButtons();
    Destroy(_camera);
  }

  private void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _camera;
  [SerializeField] private Button[] _buttons;
  [SerializeField] private GameObject[] _jobs;
  [SerializeField] private BattleSceneManager manager;
}

