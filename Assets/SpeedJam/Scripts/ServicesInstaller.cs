using Zenject;

namespace SpeedJam
{
    public class ServicesInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<ListOfGravitationalObject>().FromInstance(new ListOfGravitationalObject()).AsSingle();   
        }
    }
}