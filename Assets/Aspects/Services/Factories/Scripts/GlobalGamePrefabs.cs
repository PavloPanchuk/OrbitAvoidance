using UnityEngine;

namespace Aspects.Services.Factories.Scripts
{
    [CreateAssetMenu(fileName = "GlobalGamePrefabs", menuName = "Create Global Game Prefabs")]
    public class GlobalGamePrefabs : ScriptableObject
    {
        public GameObject player;
        public GameObject orbit;
        public GameObject enemyA;
        public GameObject jokeManiaBox;
    }
}