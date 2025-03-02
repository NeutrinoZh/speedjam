using System.Collections;
using UnityEngine;

namespace SpeedJam
{
    public class TempObject : MonoBehaviour
    {
        [SerializeField] private float _timeOfLife;

        private void Start()
        {
            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(_timeOfLife);
            Destroy(gameObject);
        }
    }
}