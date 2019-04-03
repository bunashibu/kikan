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
        .Where(_ => IsCorrectAnimationState(player)       )
        .Where(_ => !player.Debuff.State[DebuffType.Stun] )
        .Where(_ => weapon.CanInstantiate                 )
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

    private void InstantiateSkill(int i, Weapon weapon, Player player) {
      string path = "Prefabs/Skill/" + weapon.JobName + "/" + weapon.SkillNames[i];

      var offset = weapon.AppearOffset[i];
      if (player.Renderers[0].flipX)
        offset.x *= -1;
      var pos = weapon.transform.position + offset;

      var skill = PhotonNetwork.Instantiate(path, pos, Quaternion.identity, 0).GetComponent<Skill>();
      skill.SyncInit(player.Renderers[0].flipX, player.PhotonView.viewID);

      SetCoroutine(i, weapon, player);
      SkillReference.Instance.Register(skill);
      weapon.Stream.OnNextInstantiate(i);
    }

    private void SetCoroutine(int i, Weapon weapon, Player player) {
      player.SkillInfo.SetState(weapon.SkillNames[i], SkillState.Using);
      MonoUtility.Instance.StoppableDelaySec(weapon.SkillCT[i], "SkillInfoState" + i.ToString(), () => {
        player.SkillInfo.SetState(weapon.SkillNames[i], SkillState.Ready);
      });

      player.State.Rigor = true;
      MonoUtility.Instance.StoppableDelaySec(weapon.RigorCT[i], "PlayerStateRigor" + i.ToString(), () => {
        player.State.Rigor = false;
        player.SkillInfo.SetState(weapon.SkillNames[i], SkillState.Used);
      });
    }

    private bool IsCorrectAnimationState(Player player) {
      return !(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") ||
               player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Ladder"));
    }
  }
}

