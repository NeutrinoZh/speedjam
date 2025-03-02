using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameOverController _gameOverController;
        [SerializeField] private CameraController _cameraController;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<ListsOfObjects>().FromInstance(new ListsOfObjects()).AsSingle();
            Container.Bind<Controls>().FromInstance(new Controls()).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<ScoreManager>().FromInstance(new ScoreManager()).AsSingle();
            Container.Bind<GameOverController>().FromInstance(_gameOverController).AsSingle();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
        }
    }
}