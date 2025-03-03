using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace SpeedJam
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Image _blackScreen;
        [SerializeField] private float _blackScreenAnimationDuration;
        
        private Button _startButton;
        private GlobalData _globalData;

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }

        private void Awake()
        {
            _startButton = GetComponent<Button>();
        }
        
        private void Start()
        {
            _startButton.onClick.AddListener(Handle);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(Handle);
        }

        private void Handle()
        {
            _globalData.CurrentLevel = 2;
            Tween.Alpha(_blackScreen, 1f, _blackScreenAnimationDuration).OnComplete(() => SceneManager.LoadScene(_globalData.CurrentLevel));
        }
    }
}