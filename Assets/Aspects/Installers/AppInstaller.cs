using Aspects.Enemies.Scripts;
using Aspects.Orbit.Scripts;
using Aspects.Player.Scripts;
using Aspects.Services;
using Aspects.Services.Factories.Scripts;
using Aspects.Services.InputService;
using Aspects.Services.Orbit;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Aspects.Installers
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private GlobalGamePrefabs globalGamePrefabs;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<OrbitService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GlobalGamePrefabs>().FromInstance(globalGamePrefabs).AsSingle();
            Container.BindInterfacesTo<HttpClientService>().AsSingle();
            Container.BindInterfacesTo<TextureDownloaderService>().AsSingle();
            Container.BindInterfacesTo<JokeManiaService>().AsSingle();

            BindFactory<PlayerEntity, PlayerEntity.Factory>(globalGamePrefabs.player);
            BindFactory<OrbitEntity, OrbitEntity.Factory>(globalGamePrefabs.orbit);
            BindFactory<EnemyEntity, EnemyEntity.Factory>(globalGamePrefabs.enemyA);
            BindFactory<JokeManiaBox, JokeManiaBox.Factory>(globalGamePrefabs.jokeManiaBox);
            
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        private void BindFactory<TContract, TFactory>(Object prefab) where TFactory : PlaceholderFactory<TContract> =>
            Container.BindFactory<TContract, TFactory>()
                .FromComponentInNewPrefab(prefab);
    }
}