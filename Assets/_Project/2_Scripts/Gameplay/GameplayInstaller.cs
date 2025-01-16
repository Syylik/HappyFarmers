using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private AreaData _currentAreaData;

    public override void InstallBindings()
    {
        Container.Bind<AreaData>().FromInstance(_currentAreaData).AsSingle();
        Container.Bind<AreaCropData>().FromInstance(_currentAreaData.cropData).AsSingle();
        Container.Bind<AreaUpgradeData>().FromInstance(_currentAreaData.areaUpgradeData).AsSingle();

        Container.Bind<WorkerManager>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<WorkerManager>().AsSingle();
    }
}