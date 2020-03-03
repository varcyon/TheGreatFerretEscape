using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace com.VarcyonSariouGames.TheGreatFerretEscape {
    public class PhotonConnection : MonoBehaviourPunCallbacks {
        public static PhotonConnection PC;
        public TextMeshProUGUI connectionStatus;
        public TextMeshProUGUI gameVersion;

        
        public void Awake () {
            if (PhotonConnection.PC == null) {
                PhotonConnection.PC = this;
            } else {
                if (PhotonConnection.PC != this) {
                    Destroy (this.gameObject);
                }
            }
            DontDestroyOnLoad (this.gameObject);
            gameVersion.text = Application.version;
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect ();
        }
        private void FixedUpdate () {
            connectionStatus.text = PhotonNetwork.NetworkClientState.ToString ();
        }

        public override void OnConnectedToMaster () {
            //Join();
            base.OnConnectedToMaster ();

        }

        public override void OnJoinedRoom () {
            StartGame ();
            base.OnJoinedRoom ();
        }

        public override void OnJoinRandomFailed (short returnCode, string message) {
            CreatRoom ();
        }
        public void CreatRoom () {
            PhotonNetwork.CreateRoom ("");
        }
        public void Connect () {
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.ConnectUsingSettings ();
        }

        public void Join () {
            PhotonNetwork.JoinRandomRoom ();
        }

        public void StartGame () {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
                PhotonNetwork.LoadLevel (1);
            }
        }
    }

}