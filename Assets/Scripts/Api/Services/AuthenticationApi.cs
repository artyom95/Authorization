using System;
using System.Collections;
using System.Text;
using Api.Requests;
using Api.Responses;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using User;

namespace Api.Services
{
    public class AuthenticationApi
    {
        public void SendRegistrationRequest(string username, string password,
            Action<UserProfile> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.REGISTRATION_URL;

            var authenticationRequest = new AuthenticationRequest
            {
                Username = username,
                Password = password,
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };

            WebApi.Instance.StartCoroutine(SendRequest(url, authenticationRequest, onSuccess, onError));
        }

        public void SendLoginRequest(string username, string password,
            Action<UserProfile> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.LOGIN_URL;

            var authenticationRequest = new AuthenticationRequest
            {
                Username = username,
                Password = password
            };

            WebApi.Instance.StartCoroutine(SendRequest(url, authenticationRequest, onSuccess, onError));
        }

        public void SendLoginRequestByDeviceId(Action<UserProfile> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.LOGIN_URL;
            var authenticationRequest = new AuthenticationRequest
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };
            WebApi.Instance.StartCoroutine(SendRequest(url, authenticationRequest, onSuccess, onError));
        }

        private IEnumerator SendRequest(string url, AuthenticationRequest request,
            Action<UserProfile> onSuccess, Action<string> onError)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);

            var webRequest = UnityWebRequest.Post(url, jsonRequest);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(jsonRequest));

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                if (webRequest.downloadHandler.data == null)
                {
                    onError?.Invoke(webRequest.error);
                    yield break;
                }

                var message = Encoding.ASCII.GetString(webRequest.downloadHandler.data);
                var errorMessage = $"Request failed. Error: {webRequest.error}. {message}";
                onError?.Invoke(errorMessage);
                yield break;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonResponse);
            WebApi.Instance.JwtToken = response.UserProfile.JwtToken;
            onSuccess?.Invoke(response.UserProfile);
        }
    }
}