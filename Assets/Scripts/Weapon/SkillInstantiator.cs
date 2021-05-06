using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class SkillInstantiator {
    public SkillInstantiator(Weapon weapon, Player player) {
      weapon.UpdateAsObservable()
        .Where(_ => player.PhotonView.isMine              )
        .Where(_ => player.Hp.Cur.Value > 0               )
        .Where(_ => !player.State.Rigor                   )
        .Where(_ => IsSkillUsableAnimationState(player)   )
        .Where(_ => !player.Debuff.State[DebuffType.Stun] )
        .Where(_ => weapon.CanInstantiate.Value           )
        .Subscribe(_ => {
          int index = GetSkillIndex(weapon, player);

          if (index == -1) // No skill
            return;

          InstantiateSkill(index, weapon, player);
        });
    }

    private int GetSkillIndex(Weapon weapon, Player player) {
      for (int i=0; i < weapon.KeysList.Count; ++i) {
        if (player.Level.Cur.Value < weapon.RequireLv[i])
          continue;

        for (int k=0; k < weapon.KeysList[i].keys.Count; ++k) {
          if (weapon.IsUsable(i) && Input.GetKey(weapon.KeysList[i].keys[k]))
            return i;
        }
      }

      return -1;
    }

    public Skill InstantiateSkill(int i, Weapon weapon, Player player) {
      string path = "Prefabs/Skill/" + weapon.JobName + "/" + weapon.SkillNames[i];

      var offset = weapon.AppearOffset[i];
      if (player.Renderers[0].flipX)
        offset.x *= -1;
      var pos = weapon.transform.position + offset;

      var quat = player.Renderers[0].flipX ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
      var skill = PhotonNetwork.Instantiate(path, pos, quat, 0).GetComponent<Skill>();
      skill.SyncInit(player.PhotonView.viewID);

      SetCoroutine(i, weapon, player);
      SkillReference.Instance.Register(skill.viewID);
      weapon.Stream.OnNextInstantiate(i);

      return skill;
    }

    private void SetCoroutine(int i, Weapon weapon, Player player) {
      player.State.Rigor = true;

      MonoUtility.Instance.StoppableDelaySec(weapon.RigorCT[i], "PlayerStateRigor" + i.ToString(), () => {
        player.State.Rigor = false;
      });
    }

    public bool IsSkillUsableAnimationState(Player player) {
      return !(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") ||
               player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Ladder"));
    }
  }
}
