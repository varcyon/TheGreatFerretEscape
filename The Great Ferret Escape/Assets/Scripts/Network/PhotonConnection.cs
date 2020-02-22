using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

namespace com.VarcyonSariouGames.TheGreatFerretEscape {
    public class PhotonConnection : MonoBehaviourPunCallbacks {
       public TextMeshProUGUI connectionStatus; 
       public TextMeshProUGUI gameVersion;
        public void Awake()
        {
            gameVersion.text = Application.version;
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect();
        }
        private void FixedUpdate() {
            connectionStatus.text = PhotonNetwork.NetworkClientState.ToString();
        }

        public override void OnConnectedToMaster(){
            //Join();
            base.OnConnectedToMaster();
            
        }

        public override void OnJoinedRoom(){
            StartGame();
            base.OnJoinedRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message){
            CreatRoom();
        }
        public void CreatRoom(){
            PhotonNetwork.CreateRoom("");
        }
        public void Connect(){
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Join(){
            PhotonNetwork.JoinRandomRoom();
        }

        public void StartGame(){
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1){
                PhotonNetwork.LoadLevel(1);
            }
        }
    }

}