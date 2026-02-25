using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace RPG {
    public class MonsterSpawner : MonoBehaviour {
        [Serializable]
        public class MonsterWave {
            public int count;
            public int monsterLevel;
            [TextArea(2, 10)] public string spawnMessage;
        }

        private static MonsterSpawner instance = null;
        [SerializeField] private GameObject _chomperMonsterPrefab;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private MonsterWave[] _monsterWaves;
        [SerializeField] private bool _isWaveStarted = false;
        [SerializeField] private int _currentWaveID = 0;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            var transforms = GetComponentsInChildren<Transform>();
            _spawnPositions = new Transform[transforms.Length - 1];
            Array.Copy(transforms, 1, _spawnPositions, 0, _spawnPositions.Length);
        }

        public static void SpawnMonsters(int count, int monsterLevel) {
            for (int ix = 0; ix < count; ++ix) {
                SpawnAMonster(monsterLevel);
            }
        }

        private static void SpawnAMonster(int monsterLevel) {
            int spawnPositionIndex = Random.Range(0, instance._spawnPositions.Length);
            Vector3 spawnLocation = instance._spawnPositions[spawnPositionIndex].position;
            Quaternion spawnRotation = instance._spawnPositions[spawnPositionIndex].rotation;
            GameObject newMonster = Instantiate(instance._chomperMonsterPrefab, spawnLocation, spawnRotation);
            MonsterStateManager stateManager = newMonster.GetComponent<MonsterStateManager>();
            stateManager.SetLevel(monsterLevel);

            if (instance._isWaveStarted) {
                stateManager.SetForceToChase();
            }
        }

        private static IEnumerator SpawnMonstersCoroutine(int count, int monsterLevel) {
            for (int ix = 0; ix < count; ++ix) {
                yield return instance.StartCoroutine(SpawnOneMonster(0.2f, monsterLevel));
            }
        }

        private static IEnumerator SpawnOneMonster(float time, int monsterLevel) {
            yield return new WaitForSeconds(time);
            SpawnAMonster(monsterLevel);
        }

        private static IEnumerator SpawnMonstersWithDelay(float time, int count, int monsterLevel) {
            yield return new WaitForSeconds(time);
            instance.StartCoroutine(SpawnMonstersCoroutine(count, monsterLevel));
        }

        public static void StartMonsterWave() {
            if (instance._isWaveStarted) {
                MoveToNextWave();
                return;
            }

            instance._isWaveStarted = true;
            instance._currentWaveID = 0;
            int count = instance._monsterWaves[instance._currentWaveID].count;
            int monsterLevel = instance._monsterWaves[instance._currentWaveID].monsterLevel;
            instance.StartCoroutine(SpawnMonstersWithDelay(2f, count, monsterLevel));
            Dialogue.ShowDialogueTextTemporarily(instance._monsterWaves[instance._currentWaveID].spawnMessage);
        }

        private static void MoveToNextWave() {
            if (instance._currentWaveID == instance._monsterWaves.Length - 1) {
                return;
            }

            ++instance._currentWaveID;
            int count = instance._monsterWaves[instance._currentWaveID].count;
            int monsterLevel = instance._monsterWaves[instance._currentWaveID].monsterLevel;
            instance.StartCoroutine(SpawnMonstersWithDelay(3f, count, monsterLevel));
            Dialogue.ShowDialogueTextTemporarily(instance._monsterWaves[instance._currentWaveID].spawnMessage);
        }
    }
}