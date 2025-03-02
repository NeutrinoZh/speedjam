using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GlobalData>().FromInstance(new GlobalData()).AsSingle();
        }
    }
}