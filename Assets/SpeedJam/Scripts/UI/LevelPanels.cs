using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpeedJam
{
    public class LevelPanels : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;

        private int _selectedLevel = 0;

        public int SelectedLevel => _selectedLevel;
        
        private void Start()
        {
            var countOfOpenLevels = PlayerPrefs.GetInt(DataRefs.countOfOpenLevels, 1);
            
            if (countOfOpenLevels >= 3)
                countOfOpenLevels = 3;
            
            for (int i = 0; i < countOfOpenLevels; i++) 
                _buttons[i].interactable = true;
            
            EventSystem.current.firstSelectedGameObject = _buttons[0].gameObject;
            EventSystem.current.SetSelectedGameObject(_buttons[0].gameObject);
            EventSystem.current.firstSelectedGameObject = _buttons[0].gameObject;

            for (int i = 0; i < _buttons.Count; i++)
            {
                var j = i;
                _buttons[i].onClick.AddListener(() => _selectedLevel = j);
            }
        }
    }
}
