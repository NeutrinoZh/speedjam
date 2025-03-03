using System;
using System.Linq;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace SpeedJam
{
    public class AuthUser : MonoBehaviour
    {
        public event Action OnAuthenticated;
        public Action OnChangeUsername;
        
        private GlobalData _globalData;

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }
        
        private async void Awake()
        {
            try
            {
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                string result = await AuthenticationService.Instance.GetPlayerNameAsync();
                _globalData.Username = result;
                
                OnAuthenticated?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);    
            }
        }
    }
}