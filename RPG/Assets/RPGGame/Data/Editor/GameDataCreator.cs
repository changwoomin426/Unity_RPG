using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RPG {
    public class GameDataCreator {
        private static readonly string dataFolderPath = "Assets/RPGGame/Resources/Data";
        private static readonly string playerLevelDataFilePath = "Assets/RPGGame/Data/Editor/PlayerLevelData.csv";
        private static readonly string playerDataSOFilePath = "Assets/RPGGame/Resources/Data/Player Data.asset";
        private static readonly string monsterLevelDataFilePath = "Assets/RPGGame/Data/Editor/MonsterLevelData.csv";
        private static readonly string monsterDataSOFilePath = "Assets/RPGGame/Resources/Data/Monster Data.asset";

        private static void CheckAndCreateDataFolder() {
            if (!Directory.Exists(dataFolderPath)) {
                Directory.CreateDirectory(dataFolderPath);
            }
        }

        [MenuItem("RPGGame/Create Player Data")]
        private static void CreatePlayerData() {
            CheckAndCreateDataFolder();
            PlayerData playerDataSO = AssetDatabase.LoadAssetAtPath(playerDataSOFilePath, typeof(PlayerData)) as PlayerData;

            if (playerDataSO == null) {
                playerDataSO = ScriptableObject.CreateInstance<PlayerData>();
                AssetDatabase.CreateAsset(playerDataSO, playerDataSOFilePath);
            }

            string[] lines = File.ReadAllLines(playerLevelDataFilePath);
            playerDataSO.levels = new List<PlayerData.LevelData>();

            for (int ix = 1; ix < lines.Length; ++ix) {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                PlayerData.LevelData levelData = new PlayerData.LevelData();
                levelData.level = int.Parse(data[0]);
                levelData.maxHP = float.Parse(data[1]);
                levelData.requiredExp = float.Parse(data[2]);
                playerDataSO.levels.Add(levelData);
            }

            EditorUtility.SetDirty(playerDataSO);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create Monster Data")]
        private static void CreateMonsterData() {
            CheckAndCreateDataFolder();
            MonsterData monsterDataSO = AssetDatabase.LoadAssetAtPath(monsterDataSOFilePath, typeof(MonsterData)) as MonsterData;

            if (monsterDataSO == null) {
                monsterDataSO = ScriptableObject.CreateInstance<MonsterData>();
                AssetDatabase.CreateAsset(monsterDataSO, monsterDataSOFilePath);
            }

            string[] lines = File.ReadAllLines(monsterLevelDataFilePath);
            monsterDataSO.levels = new List<MonsterData.LevelData>();

            for (int ix = 1; ix < lines.Length; ++ix) {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                MonsterData.LevelData levelData = new MonsterData.LevelData();
                levelData.level = int.Parse(data[0]);
                levelData.maxHP = float.Parse(data[1]);
                levelData.attack = float.Parse(data[2]);
                levelData.defense = float.Parse(data[3]);
                levelData.gainExp = float.Parse(data[4]);
                monsterDataSO.levels.Add(levelData);
            }

            EditorUtility.SetDirty(monsterDataSO);
            AssetDatabase.SaveAssets();
        }
    }
}