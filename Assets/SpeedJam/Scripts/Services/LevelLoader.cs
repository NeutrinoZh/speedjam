using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpeedJam
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private AuthUser _authUser;

        private GlobalData _globalData;

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }
        
        private void Start()
        {
            _authUser.OnChangeUsername += Handle;
        }

        private void OnDestroy()
        {
            _authUser.OnChangeUsername -= Handle;
        }
        
        private void Handle()
        {
            _globalData.CurrentLevel = 1;
            SceneManager.LoadScene(_globalData.CurrentLevel);
        }
    }
}