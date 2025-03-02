using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

namespace SpeedJam
{
    public class SetupListStars : MonoBehaviour
    {
        private ListsOfObjects _listsOfObjects;
        private ScoreManager _scoreManager;
        
        [Inject]
        public void Construct(ListsOfObjects listsOfObjects, ScoreManager scoreManager)
        {
            _listsOfObjects = listsOfObjects;
            _scoreManager = scoreManager;
        }

        private void Awake()
        {
            foreach (Transform item in transform)
                if (item.TryGetComponent(out Star obj))
                    _listsOfObjects.Stars.Add(obj);

            _scoreManager.MaxScore = _listsOfObjects.Stars.Count;
            
            Destroy(this);
        }
    }
}