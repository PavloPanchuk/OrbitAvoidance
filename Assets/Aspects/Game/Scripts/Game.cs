using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Aspects.Enemies.Scripts;
using Aspects.Orbit.Scripts;
using Aspects.Player.Scripts;
using Aspects.Services.Orbit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Aspects.Game.Scripts
{
    public class Game : MonoBehaviour
    {
        private const string MainOrbitKey = "main_orbit";

        [SerializeField] private RectTransform jokeManiaBoxPlaceholder;

        private EnemyEntity.Factory _enemyFactory;
        private PlayerEntity _player;

        [Inject]
        public void Construct(
            IOrbitService orbitService,
            OrbitEntity.Factory orbitFactory,
            PlayerEntity.Factory playerFactory,
            EnemyEntity.Factory enemyFactory,
            JokeManiaBox.Factory jokeManiaBoxFactory)
        {
            _enemyFactory = enemyFactory;
            var mainOrbit = orbitFactory.Create(transform);
            mainOrbit.Radius = 2.5f;
            orbitService.AddOrbit(MainOrbitKey, mainOrbit);

            _player = playerFactory.Create(transform);

            var orbitChild = orbitService.AddChild(MainOrbitKey, _player.gameObject);

            StartCoroutine(EnemySpawnerRoutine());
            // EnemySpawnerAsync();
            // EnemySpawnerAsyncTask();
            var jokeManiaBox = jokeManiaBoxFactory.Create(jokeManiaBoxPlaceholder);
            var jokeManiaBoxTransform = jokeManiaBox.GetComponent<RectTransform>();
            jokeManiaBoxTransform.transform.position = jokeManiaBoxPlaceholder.position;
        }

        //-------------------Multithreading and async tests------------------//
        
        private async void Update()
        {
            if (Input.GetKeyUp(KeyCode.S))
                await IterationUniTaskAsync(this.GetCancellationTokenOnDestroy());
        }

        private IEnumerator EnemySpawnerRoutine()
        {
            var count = 0;
            while (count < 2000)
            {
                SpawnEnemy();
                count += 1;
                yield return new WaitForSeconds(Random.Range(0f, 3f));
            }

            yield return null;
        }

        private async UniTask EnemySpawnerAsyncUniTask()
        {
            var count = 0;
            while (count < 2000)
            {
                SpawnEnemy();
                count += 1;
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0f, 3f)));
            }
        }

        private async Task EnemySpawnerAsyncTask()
        {
            var count = 0;
            while (count < 2000)
            {
                SpawnEnemy();
                count += 1;
                await Task.Delay(TimeSpan.FromSeconds(Random.Range(0f, 3f)));
            }
        }

        private EnemyEntity SpawnEnemy()
        {
            var enemy = _enemyFactory.Create(transform);
            enemy.SetSpeed(Random.Range(0f, 5f));
            enemy.SetDirection(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

            return enemy;
        }

        private async UniTask IterationUniTaskAsync(CancellationToken ct)
        {
            try
            {
                // await IterationThreadPool(ct);
                await IterationYield(ct);
                await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: ct);
                Debug.Log("UniTask is completed");
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        private async UniTask IterationYield(CancellationToken ct)
        {
            for (int i = 0; i < 100000; i++)
            {
                if (ct.IsCancellationRequested)
                    break;
                await UniTask.Yield();
                Debug.Log($"Test Async: {i} | Current ThreadId {Thread.CurrentThread.ManagedThreadId}");
            }
        }
        
        private async UniTask IterationThreadPool(CancellationToken ct)
        {
            await UniTask.RunOnThreadPool(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    if (ct.IsCancellationRequested)
                        break;
                    Debug.Log($"Test Async: {i} | Current ThreadId {Thread.CurrentThread.ManagedThreadId}");
                }
            }, cancellationToken: ct);
        }
    }
}