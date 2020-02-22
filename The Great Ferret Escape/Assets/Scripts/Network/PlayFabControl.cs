using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.VarcyonSariouGames.TheGreatFerretEscape {
    public class PlayFabControl : MonoBehaviour {
        public static PlayFabControl PFC;
        public TextMeshProUGUI username;
        public TextMeshProUGUI displayName;
        public GameObject displayNameGO;
        public TextMeshProUGUI password;
        public TextMeshProUGUI email;
        public GameObject emailGO;
        public TextMeshProUGUI message;
        public TextMeshProUGUI registerButton;
        LoginWithPlayFabRequest loginWithPlayFabRequest;
        public bool isAuthenticated = false;
        public GameObject loginUI;
        private void OnEnable () {
            if (PlayFabControl.PFC == null) {
                PlayFabControl.PFC = this;
            } else {
                if (PlayFabControl.PFC != this) {
                    Destroy (this.gameObject);
                }
            }
            DontDestroyOnLoad (this.gameObject);

        }
        void Start () {
            emailGO.SetActive (false);
            displayNameGO.SetActive (false);
        }
        public void HideLogin () {
            loginUI.SetActive (false);
        }

        #region Login
        public void LoginPF () {
            loginWithPlayFabRequest = new LoginWithPlayFabRequest ();
            loginWithPlayFabRequest.Username = username.text;
            loginWithPlayFabRequest.Password = password.text;
            PlayFabClientAPI.LoginWithPlayFab (loginWithPlayFabRequest, result => {
                isAuthenticated = true;
                message.text = "Wecome " + username.text + "!";
                StartCoroutine (waitAfterLogin ());
            }, error => {
                isAuthenticated = false;
                if (error.ErrorMessage == "User not found") {
                    message.text = "Please register with your email.";
                    emailGO.SetActive (true);
                    displayNameGO.SetActive (true);
                } else {
                    message.text = "Failed to login![" + error.ErrorMessage + "]";
                }
            }, null);
        }

        public void RegisterPF () {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest ();
            request.Email = email.text;
            request.Username = username.text;
            request.Password = password.text;
            PlayFabClientAPI.RegisterPlayFabUser (request, result => {
                PlayFabClientAPI.UpdateUserTitleDisplayName (new UpdateUserTitleDisplayNameRequest { DisplayName = displayName.text }, OnUpdateDisplayNameSuccess, OnError);
                message.text = "Your account has been created![Loging in..]";
                StartCoroutine (waitToLogin ());
            }, error => {
                message.text = "Please enter your email address.";
                emailGO.SetActive (true);
                registerButton.text = "Continue";

            });
        }
        ////////////////////////////////////////////////
        //TODO: Steam Login when partner, need APP ID//
        // 100 dollar fee
        ///////////////////////////////////////////////

        ////////////////////////////////////////////////
        //TODO: Google Login when partner, need APP ID//
        //25 dollar developer fee
        ///////////////////////////////////////////////
        void OnUpdateDisplayNameSuccess (UpdateUserTitleDisplayNameResult result) {
            Debug.Log (result.DisplayName + " is your new display name.");
        }

        void OnError (PlayFabError error) {
            Debug.Log (error.GenerateErrorReport ());
        }
        IEnumerator waitToLogin () {
            yield return new WaitForSeconds (2f);
            LoginPF ();
        }
        IEnumerator waitAfterLogin () {
            yield return new WaitForSeconds (2f);
            HideLogin ();
        }

        #endregion Login

    }
}