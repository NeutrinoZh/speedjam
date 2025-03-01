using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class SetupListObject : MonoBehaviour
    {
        private ListOfGravitationalObject _listOfObjects;
        
        [Inject]
        public void Construct(ListOfGravitationalObject listOfObjects)
        {
            _listOfObjects = listOfObjects;
        }

        private void Awake()
        {
            foreach (Transform item in transform)
                if (item.TryGetComponent(out GravitationalObject obj))
                    _listOfObjects.Objects.Add(obj);
            
            Destroy(this);
        }
    }
}