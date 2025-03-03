using PrimeTween;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedJam {
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private Button _enableButton;
        [SerializeField] private Button _disableButton;

        private void Start()
        {
            _enableButton.onClick.AddListener(Show);
            _disableButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _enableButton.onClick.RemoveListener(Show);
            _disableButton.onClick.RemoveListener(Hide);
        }

        private void Show()
        {
            Tween.LocalPositionY(transform, 0, 0.4f);
        }

        private void Hide()
        {
            Tween.LocalPositionY(transform, -540, 0.4f);
        }
    }
}