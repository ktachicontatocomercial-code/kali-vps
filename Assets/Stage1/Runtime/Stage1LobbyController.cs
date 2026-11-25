using UnityEngine;

namespace BattleRoyale.Stage1
{
    public class Stage1LobbyController : MonoBehaviour
    {
        private PlayerProfileData profile;
        private string pendingName = "";
        private bool editingName;

        private void Awake()
        {
            profile = LocalProfileStore.Load();
            editingName = profile.playerId == "BR-000000";
            pendingName = profile.playerName == "Operador" ? "" : profile.playerName;
        }

        private void OnGUI()
        {
            GUI.skin.label.fontSize = 20;
            GUI.skin.button.fontSize = 18;
            GUI.skin.textField.fontSize = 20;
            if (editingName)
            {
                const float width = 420f;
                const float height = 190f;
                var box = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
                GUI.Box(box, "CRIAR PERFIL LOCAL");
                GUI.Label(new Rect(box.x + 25, box.y + 45, width - 50, 30), "Digite o nome do jogador:");
                pendingName = GUI.TextField(new Rect(box.x + 25, box.y + 82, width - 50, 36), pendingName, 18);
                if (GUI.Button(new Rect(box.x + 25, box.y + 132, width - 50, 38), "CONTINUAR") && !string.IsNullOrWhiteSpace(pendingName))
                {
                    profile.playerName = pendingName.Trim();
                    profile.playerId = LocalProfileStore.CreateUniqueId();
                    LocalProfileStore.Save(profile);
                    editingName = false;
                }
                return;
            }

            GUI.Label(new Rect(25, 20, 400, 35), $"Jogador: {profile.playerName}");
            GUI.Label(new Rect(25, 55, 400, 35), $"ID: {profile.playerId}");
            if (GUI.Button(new Rect(25, 95, 220, 38), "EDITAR NOME")) editingName = true;
        }
    }
}
