using System;
using Api;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using User;

namespace Views
{
    public class AuthenticationView : MonoBehaviour
    {
        public event Action<MonoBehaviour, UserProfile> Authorized;

        [SerializeField] 
        private TMP_InputField _usernameInput;
        [SerializeField] 
        private TMP_InputField _passwordInput;
        [SerializeField] 
        private GameObject _preloader;

      
        [SerializeField] private GameObject _panelInvalidUsernameOrPassword;
        [SerializeField] private GameObject _failPanel;
        [SerializeField] private GameObject _panelLoginOrPasswordIsEmpty;

        public void Register()
        {
            if (!ValidateInput())
            {
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendRegistrationRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        public void LogIn()
        {
            if (!ValidateInput())
            {
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendLoginRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(_usernameInput.text) || string.IsNullOrEmpty(_passwordInput.text))
            {
                Debug.LogError("Login or Password is empty.");
                _panelLoginOrPasswordIsEmpty.SetActive(true);
                return false;
            }

            return true;
        }

        private void OnSuccess(UserProfile userProfile)
        {
            HidePreloader();
            Authorized?.Invoke(this, userProfile);
        }

        private void OnError(string errorMessage)
        {
           HidePreloader();
            Debug.LogError(errorMessage);
            if (errorMessage.Equals("Request failed. Error: HTTP/1.1 400 Bad Request. Invalid username or password"))
            {
               _panelInvalidUsernameOrPassword.SetActive(true);
            }

            if (errorMessage.Equals("Cannot connect to destination host"))
            {
               _failPanel.SetActive(true); 
            }

        }

        private void ShowPreloader()
        {
            _preloader.gameObject.SetActive(true);
        }

        private void HidePreloader()
        {
            _preloader.gameObject.SetActive(false);
        }
    }
}