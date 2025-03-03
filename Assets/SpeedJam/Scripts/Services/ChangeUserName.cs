using System;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class ChangeUserName : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private AuthUser _authUser;
        private GlobalData _globalData;

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }
        
        private void Start()
        {
            _userName.onSubmit.AddListener(OnSubmit);
        }

        private void OnDestroy()
        {
            _userName.onSubmit.RemoveListener(OnSubmit);
        }

        private async void OnSubmit(string value)
        {
            try
            {
                string username = $"{value.ToLower()}";
            
                string result = await AuthenticationService.Instance.GetPlayerNameAsync();;
                string oldUsername = result[..^5];

                if (username != oldUsername)
                    await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
                
                result = await AuthenticationService.Instance.GetPlayerNameAsync();
                _globalData.Username = result;
                
                _authUser.OnChangeUsername?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}