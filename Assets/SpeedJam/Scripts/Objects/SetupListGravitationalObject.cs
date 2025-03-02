using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class SetupListGravitationalObject : MonoBehaviour
    {
        private ListsOfObjects _listsOfObjects;
        
        [Inject]
        public void Construct(ListsOfObjects listsOfObjects)
        {
            _listsOfObjects = listsOfObjects;
        }

        private void Awake()
        {
            foreach (Transform item in transform)
                if (item.TryGetComponent(out GravitationalObject obj))
                    _listsOfObjects.GravitationalObjects.Add(obj);
            
            Destroy(this);
        }
    }
}