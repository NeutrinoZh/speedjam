using System;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class UserNameLabel : MonoBehaviour
    {
        [SerializeField] private AuthUser _user;
        private GlobalData _globalData;
        private TextMeshProUGUI _label;

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _label.text = _globalData.Username;
        }
    }
}