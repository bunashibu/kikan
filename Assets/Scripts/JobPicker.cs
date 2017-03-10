using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    float x = 37.0f;
    float y = -2.5f;
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
      x *= -1;

    _player = PhotonNetwork.Instantiate("Prehabs/Player", new Vector3(x, y, 0), Quaternion.identity, 0);
    var job = PhotonNetwork.Instantiate("Prehabs/Job/" + _jobs[n].name, new Vector3(0, 0, 0), Quaternion.identity, 0);

    //photonView.RPC("SetWeaponAsChild", PhotonTargets.AllViaServer, _player.transform.position);
    job.transform.SetParent(_player.transform, false);

    InitPlayerHealthSystem(n);
    InitPlayerStatus(n);
    InitPlayerMovement(n);

    DisableAllButtons();
    Destroy(_camera);
  }

  /*
  [PunRPC]
  public void SetWeaponAsChild(Vector3 vec3) {
    job.transform.SetParent(_player.transform, false);
  }
  */

  private void InitPlayerHealthSystem(int n) {
    var health = ScriptableObject.CreateInstance<Health>();
    health.Init(_data[n].life, _data[n].life);

    _bar = Instantiate(_bar) as Bar;
    _bar.transform.SetParent(_canvas.transform, false);

    var hs = _player.GetComponent<HealthSystem>();
    hs.Init(health, _bar);
    hs.Show();
  }

  private void InitPlayerStatus(int n) {
    var status = _player.GetComponent<PlayerStatus>();

    status.atk = _data[n].atk;
    status.dfn = _data[n].dfn;
    status.spd = _data[n].spd;
    status.jmp = _data[n].jmp;
  }

  private void InitPlayerMovement(int n) {
    var linearMove = _player.GetComponent<GroundLinearMove>();
    var jump = _player.GetComponent<GroundJump>();

    linearMove.SetForce(_data[n].spd);
    jump.SetForce(_data[n].jmp);
  }

  private void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _camera;
  [SerializeField] private Button[] _buttons;
  [SerializeField] private GameObject[] _jobs;
  [SerializeField] private JobStatus[] _data;
  [SerializeField] private Canvas _canvas;
  [SerializeField] private Bar _bar;
}

