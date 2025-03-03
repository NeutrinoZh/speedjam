using PrimeTween;
using UnityEngine;

namespace SpeedJam
{
    public class LogoAnimation : MonoBehaviour
    {
        private void Start()
        {
            Tween.Rotation(transform, Quaternion.Euler(0, 0, 5), 3f, cycles:-1, cycleMode: CycleMode.Yoyo);
        }
    }
}