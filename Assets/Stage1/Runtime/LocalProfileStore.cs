using System;
using System.IO;
using UnityEngine;

namespace BattleRoyale.Stage1
{
    [Serializable]
    public class PlayerProfileData
    {
        public string playerName = "Operador";
        public string playerId = "BR-000000";
        public int normalCurrency = 1000;
        public int specialCurrency = 70;
        public int currentRank = 0;
        public int highestRank = 0;
        public int masterCount = 0;
        public string avatarId = "default";
    }

    public static class LocalProfileStore
    {
        private static string FilePath => Path.Combine(Application.persistentDataPath, "player_profile.json");

        public static PlayerProfileData Load()
        {
            if (!File.Exists(FilePath)) return new PlayerProfileData();
            try { return JsonUtility.FromJson<PlayerProfileData>(File.ReadAllText(FilePath)) ?? new PlayerProfileData(); }
            catch (Exception e) { Debug.LogWarning($"Falha ao ler perfil local: {e.Message}"); return new PlayerProfileData(); }
        }

        public static void Save(PlayerProfileData data)
        {
            File.WriteAllText(FilePath, JsonUtility.ToJson(data, true));
        }

        public static string CreateUniqueId()
        {
            return "BR-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
        }
    }
}
