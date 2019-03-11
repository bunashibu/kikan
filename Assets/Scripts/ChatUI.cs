using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(PhotonView))]
  public class ChatUI : Photon.MonoBehaviour {
    public Rect GuiRect = new Rect(0, 0, 250, 300);
    public bool IsVisible = true;
    public bool AlignBottom = false;
    public static List<string> messages = new List<string>();
    private string inputLine = "";
    private Vector2 scrollPos = Vector2.zero;
    private static readonly int maxMessageCount = 100;

    public static readonly string ChatRPC = "Chat";

    public void Start() {
      if (this.AlignBottom) {
        this.GuiRect.y = Screen.height - this.GuiRect.height;
      }

      scrollPos.y = Mathf.Infinity;
    }

    public void OnGUI() {
      if (!this.IsVisible || !PhotonNetwork.inRoom)
        return;

      if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.LeftArrow || Event.current.keyCode == KeyCode.RightArrow)) {
        if (string.IsNullOrEmpty(this.inputLine)) {
          GUI.FocusControl("");
        }
      }

      if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return)) {
        if (!string.IsNullOrEmpty(this.inputLine)) {
          SendProcess();
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

      if (GUILayout.Button("Send", GUILayout.ExpandWidth(false)))
        SendProcess();

      GUILayout.EndHorizontal();
      GUILayout.EndArea();
    }

    private void SendProcess() {
      this.photonView.RPC("Chat", PhotonTargets.All, this.inputLine);

      var tmp = PhotonNetwork.player.CustomProperties["ViewID"];
      if (tmp != null) {
        int speakerViewID = (int)tmp;
        this.photonView.RPC("UpdatePopupRemark", PhotonTargets.All, PhotonNetwork.player.NickName, this.inputLine, speakerViewID);
      }

      this.inputLine = "";
      GUI.FocusControl("ChatInput");
    }

    [PunRPC]
    public void Chat(string newLine, PhotonMessageInfo mi) {
      string senderName = "anonymous";

      if (mi.sender != null) {
        if (!string.IsNullOrEmpty(mi.sender.NickName))
          senderName = mi.sender.NickName;
        else
          senderName = "player " + mi.sender.ID;
      }

      messages.Add(senderName +": " + newLine);
      scrollPos.y = Mathf.Infinity;
    }

    public void AddLine(string newLine) {
      if (messages.Count + 1 > maxMessageCount)
        messages = messages.GetRange(1, maxMessageCount - 1);

      messages.Add(newLine);
    }

    [PunRPC]
    private void UpdatePopupRemark(string senderName, string message, int viewID) {
      var view = PhotonView.Find(viewID);
      if (view == null) return;

      var speakerObj = view.gameObject;
      if (speakerObj == null) return;

      var speaker = speakerObj.GetComponent<ISpeaker>();
      if (speaker == null) return;

      speaker.PopupRemark.Set(senderName + " : " + message, viewID);
    }
  }
}

