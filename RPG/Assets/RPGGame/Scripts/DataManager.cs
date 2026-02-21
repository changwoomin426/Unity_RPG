using UnityEngine;

namespace RPG {
    [DefaultExecutionOrder(-100)]
    public class DataManager : MonoBehaviour {
        public static DataManager Instance { get; private set; } = null;
        public PlayerData playerData { get; private set; }
        public MonsterData monsterData { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                Initialize();
            } else {
                Destroy(gameObject);
            }
        }

        private void Initialize() {
            if (playerData == null) {
                playerData = Resources.Load<PlayerData>("Data/Player Data");

                if (playerData.levels.Count == 0) {
                    Debug.LogError("플레이어의 레벨 데이터가 초기화되지 않았습니다.");
                }
            }

            if (monsterData == null) {
                monsterData = Resources.Load<MonsterData>("Data/Monster Data");

                if (monsterData.levels.Count == 0) {
                    Debug.LogError("몬스터의 레벨 데이터가 초기화되지 않았습니다.");
                }
            }
        }
    }
}