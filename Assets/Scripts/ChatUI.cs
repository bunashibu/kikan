using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(PhotonView))]
  public class ChatUI : Photon.MonoBehaviour {
    public Rect GuiRect = new Rect(0, 0, 250, 300);
    public bool IsVisible = true;
    public bool AlignBottom = false;
    public List<string> messages = new List<string>();
    private string inputLine = "";
    private Vector2 scrollPos = Vector2.zero;

    public static readonly string ChatRPC = "Chat";

    public void Start() {
      if (this.AlignBottom) {
        this.GuiRect.y = Screen.height - this.GuiRect.height;
      }
    }

    public void OnGUI() {
      if (!this.IsVisible || !PhotonNetwork.inRoom)
        return;

      if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return)) {
        if (!string.IsNullOrEmpty(this.inputLine)) {
          int speakerViewID = (int)PhotonNetwork.player.CustomProperties["ViewID"];
          this.photonView.RPC("Chat", PhotonTargets.All, this.inputLine, speakerViewID);
          this.inputLine = "";
          GUI.FocusControl("");
          return; // printing the now modified list would result in an error. to avoid this, we just skip this single frame
        }
        else {
          GUI.FocusControl("ChatInput");
        }
      }

      GUI.Box(this.GuiRect, "");

      GUI.SetNextControlName("");
      GUILayout.BeginArea(this.GuiRect);

      scrollPos = GUILayout.BeginScrollView(scrollPos);
      GUILayout.FlexibleSpace();
      for (int i = 0; i < messages.Count; ++i) {
        GUILayout.Label(messages[i]);
      }
      GUILayout.EndScrollView();

      GUILayout.BeginHorizontal();
      GUI.SetNextControlName("ChatInput");
      inputLine = GUILayout.TextField(inputLine);
      if (GUILayout.Button("Send", GUILayout.ExpandWidth(false))) {
        int speakerViewID = (int)PhotonNetwork.player.CustomProperties["ViewID"];
        this.photonView.RPC("Chat", PhotonTargets.All, this.inputLine, speakerViewID);

        this.inputLine = "";
        GUI.FocusControl("");
      }
      GUILayout.EndHorizontal();
      GUILayout.EndArea();
    }

    [PunRPC]
    public void Chat(string newLine, int viewID, PhotonMessageInfo mi) {
      string senderName = "anonymous";

      if (mi.sender != null) {
        if (!string.IsNullOrEmpty(mi.sender.NickName))
          senderName = mi.sender.NickName;
        else
          senderName = "player " + mi.sender.ID;
      }

      this.messages.Add(senderName +": " + newLine);
      scrollPos.y = Mathf.Infinity;

      UpdatePopupRemark(senderName, newLine, viewID);
    }

    public void AddLine(string newLine) {
      this.messages.Add(newLine);
    }

    private void UpdatePopupRemark(string senderName, string message, int viewID) {
      var speaker = PhotonView.Find(viewID).gameObject.GetComponent<ISpeaker>();
      speaker.PopupRemark.Set(senderName + " : " + message, viewID);
    }
  }
}

