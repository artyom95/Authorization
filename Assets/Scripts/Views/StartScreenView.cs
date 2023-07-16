using System;
using Api;
using UnityEngine;
using User;

namespace Views
{
    public class StartScreenView : MonoBehaviour
    {
        public event Action<UserProfile> Authorized;
        [SerializeField] private GameObject _loginPopup;
        [SerializeField] private GameObject _lobbyScreen;
        private AuthenticationView _authenticationView;

        private void Start()
        {
            _authenticationView = GetComponent<AuthenticationView>();
            _authenticationView.Authorized += OnAuthorized;

            WebApi.Instance.AuthenticationAPI.SendLoginRequestByDeviceId(OnSuccess, OnError);
        }

        public void OnGameStarted()
        {
            _loginPopup.gameObject.SetActive(true);
        }

        private void OnSuccess(UserProfile userProfile)
        {
            Authorized?.Invoke(userProfile);
            LoadLobbyScreen();
        }

        private void OnError(string errorMessage)
        {
            Debug.LogError(errorMessage);
           
        }

        private void OnAuthorized(MonoBehaviour popup, UserProfile userProfile)
        {
            Authorized?.Invoke(userProfile);

            popup.gameObject.SetActive(false);
            LoadLobbyScreen();
        }

        private void LoadLobbyScreen()
        {
            gameObject.SetActive(false);
            _lobbyScreen.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _authenticationView.Authorized -= OnAuthorized;
        }
    }
}