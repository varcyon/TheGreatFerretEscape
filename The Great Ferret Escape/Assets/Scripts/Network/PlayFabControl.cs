using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace com.VarcyonSariouGames.TheGreatFerretEscape {
    public class PlayFabControl : MonoBehaviour {
        public static PlayFabControl PFC;
        public TMP_InputField username;
        public TMP_InputField displayName;
        public TMP_InputField password;
        public TMP_InputField email;
        public TextMeshProUGUI message;
        public TextMeshProUGUI registerButton;
        public GameObject emailGO;
        public GameObject displayNameGO;
        LoginWithPlayFabRequest loginWithPlayFabRequest;
        string twitchAccessToken;
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
            PlayFabClientAPI.LoginWithPlayFab (new LoginWithPlayFabRequest () {
                Username = username.text,
                    Password = password.text,
                    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams () {
                        GetPlayerProfile = true,
                        GetPlayerStatistics = true,
                        GetCharacterList = true,
                        GetCharacterInventories = true,
                        GetUserData = true
                    }
            }, result => {
                
                isAuthenticated = true;
                message.text = "Wecome " + username.text + "!";
                HideLogin ();
                MPData.MPD.displayName = result.InfoResultPayload.PlayerProfile.DisplayName;
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
            request.DisplayName = displayName.text;
            request.Email = email.text;
            request.Username = username.text;
            request.Password = password.text;
            PlayFabClientAPI.RegisterPlayFabUser (request, result => {
                message.text = "Your account has been created![Loging in..]";
                StartCoroutine (waitToLogin ());
            }, error => {
                message.text = "Please enter your email address and display name.";
                emailGO.SetActive (true);
                displayNameGO.SetActive (true);
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

        //// Retrieve Players Profile/////
        public void GetPlayerProfile (string playFabId) {
            PlayFabClientAPI.GetPlayerProfile (new GetPlayerProfileRequest () {
                    PlayFabId = playFabId,
                        // ProfileConstraints = new PlayerProfileViewConstraints () {
                        //     ShowDisplayName = true
                        // }
                },
                result => Debug.Log (result.PlayerProfile.Statistics),
                error => Debug.LogError (error.GenerateErrorReport ()));
        }

        void OnError (PlayFabError error) {
            Debug.Log (error.GenerateErrorReport ());
        }
        IEnumerator waitToLogin () {
            Debug.Log ("logging in..");
            yield return new WaitForSeconds (2f);
            LoginPF ();
        }

        #endregion Login

    }
}