using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RPG {
    public class GameDataCreator {
        private static readonly string dataFolderPath = "Assets/RPGGame/Resources/Data";
        private static readonly string playerLevelDataFilePath = "Assets/RPGGame/Data/Editor/PlayerLevelData.csv";
        private static readonly string playerDataSOFilePath = "Assets/RPGGame/Resources/Data/Player Data.asset";
        private static readonly string monsterLevelDataFilePath = "Assets/RPGGame/Data/Editor/MonsterLevelData.csv";
        private static readonly string monsterDataSOFilePath = "Assets/RPGGame/Resources/Data/Monster Data.asset";
        private static readonly string questDataFilePath = "Assets/RPGGame/Data/Editor/QuestData.csv";
        private static readonly string questDataSOFilePath = "Assets/RPGGame/Resources/Data/Quest Data.asset";
        private static readonly string npcDataFilePath = "Assets/RPGGame/Data/Editor/NPCData.csv";
        private static readonly string npcDataSOFilePath = "Assets/RPGGame/Resources/Data/NPC Data.asset";
        private static readonly string grenadierLevelDataFilePath = "Assets/RPGGame/Data/Editor/GrenadierLevelData.csv";
        private static readonly string grenadierDataSOFilePath = "Assets/RPGGame/Resources/Data/Grenadier Monster Data.asset";

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

        [MenuItem("RPGGame/Create Quest Data")]
        private static void CreateQuestData() {
            QuestData questDataSO = AssetDatabase.LoadAssetAtPath(questDataSOFilePath, typeof(QuestData)) as QuestData;

            if (questDataSO == null) {
                questDataSO = ScriptableObject.CreateInstance<QuestData>();
                AssetDatabase.CreateAsset(questDataSO, questDataSOFilePath);
            }

            string[] lines = File.ReadAllLines(questDataFilePath);
            for (int ix = 1; ix < lines.Length; ++ix) {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                QuestData.Quest quest = new QuestData.Quest();
                quest.questID = int.Parse(data[0]);
                quest.questTitle = data[1];
                quest.type = (QuestData.EType)Enum.Parse(typeof(QuestData.EType), data[2]);
                quest.targetType = (QuestData.ETargetType)Enum.Parse(typeof(QuestData.ETargetType), data[3]);
                quest.countToComplete = int.Parse(data[4]);
                quest.exp = float.Parse(data[5]);
                quest.questBeginText = data[6];
                quest.questProgressText = data[7];
                quest.smallTalk = data[8];
                quest.startCondition = int.Parse(data[9]);
                quest.nextQuestID = int.Parse(data[10]);
                quest.npcID = int.Parse(data[11]);
                quest.monsterLevel = int.Parse(data[12]);
                questDataSO._quests.Add(quest);
            }

            EditorUtility.SetDirty(questDataSO);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create NPC Data")]
        private static void CreateNPCData() {
            NPCData npcDataSO = AssetDatabase.LoadAssetAtPath(npcDataFilePath, typeof(NPCData)) as NPCData;

            if (npcDataSO == null) {
                npcDataSO = ScriptableObject.CreateInstance<NPCData>();
                AssetDatabase.CreateAsset(npcDataSO, npcDataSOFilePath);
            }

            string[] lines = File.ReadAllLines(npcDataFilePath);
            npcDataSO._attributes = new List<NPCData.Attribute>();

            for (int ix = 1; ix < lines.Length; ++ix) {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                NPCData.Attribute attribute = new NPCData.Attribute();
                attribute.id = int.Parse(data[0]);
                attribute.name = data[1];
                attribute.interactionSight = float.Parse(data[2]);
                npcDataSO._attributes.Add(attribute);
            }

            EditorUtility.SetDirty(npcDataSO);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create Grenadier Monster Data")]
        private static void CreateGrenadierMonsterData() {
            MonsterData grenadierDataSO = AssetDatabase.LoadAssetAtPath(grenadierDataSOFilePath, typeof(MonsterData)) as MonsterData;

            if (grenadierDataSO == null) {
                grenadierDataSO = ScriptableObject.CreateInstance<MonsterData>();
                AssetDatabase.CreateAsset(grenadierDataSO, grenadierDataSOFilePath);
            }

            List<string> lines = File.ReadLines(grenadierLevelDataFilePath).ToList();
            grenadierDataSO.levels = new List<MonsterData.LevelData>();

            for (int ix = 1; ix < lines.Count; ++ix) {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                MonsterData.LevelData levelData = new MonsterData.LevelData();
                levelData.level = int.Parse(data[0]);
                levelData.maxHP = float.Parse(data[1]);
                levelData.attack = float.Parse(data[2]);
                levelData.rangeAttack = float.Parse(data[3]);
                levelData.defense = float.Parse(data[4]);
                levelData.gainExp = float.Parse(data[5]);
                grenadierDataSO.levels.Add(levelData);
            }

            EditorUtility.SetDirty(grenadierDataSO);
            AssetDatabase.SaveAssets();
        }
    }
}