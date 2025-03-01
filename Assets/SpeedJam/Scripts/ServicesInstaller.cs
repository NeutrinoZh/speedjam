using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerOnGround _playerOnGround;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<ListOfGravitationalObject>().FromInstance(new ListOfGravitationalObject()).AsSingle();   
            Container.Bind<Controls>().FromInstance(new Controls()).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<PlayerOnGround>().FromInstance(_playerOnGround).AsSingle();
        }
    }
}