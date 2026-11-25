using System.IO;
using BattleRoyale.Stage1;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Stage1ProjectBuilder
{
    private const string SceneFolder = "Assets/Stage1/Scenes";
    private const string ScenePath = SceneFolder + "/Stage1_Lobby.unity";

    [MenuItem("Battle Royale/Criar-Reconstruir Etapa 1")]
    public static void BuildStage1()
    {
        Directory.CreateDirectory(SceneFolder);
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientLight = new Color(0.12f, 0.16f, 0.22f);
        CreateCamera();
        CreateLighting();
        CreateLobbyCharacter();
        CreateCanvas();
        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.Refresh();
        Debug.Log("Etapa 1 criada em " + ScenePath);
    }

    private static void CreateCamera()
    {
        var go = new GameObject("LobbyCamera");
        go.tag = "MainCamera";
        var camera = go.AddComponent<Camera>();
        camera.transform.position = new Vector3(0f, 1.5f, -7f);
        camera.transform.LookAt(new Vector3(0f, 1.2f, 0f));
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.035f, 0.055f, 0.09f);
    }

    private static void CreateLighting()
    {
        var go = new GameObject("LobbyLight");
        var light = go.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        light.color = new Color(0.75f, 0.85f, 1f);
        go.transform.rotation = Quaternion.Euler(35f, -25f, 0f);
    }

    private static void CreateLobbyCharacter()
    {
        var importedCharacter = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Stage1/Art/Characters/Superhero_Male_FullBody.fbx");
        if (importedCharacter != null)
        {
            var character = (GameObject)PrefabUtility.InstantiatePrefab(importedCharacter);
            character.name = "LobbyCharacter_Quaternius";
            character.transform.position = new Vector3(0f, 0f, 0f);
            character.transform.localScale = Vector3.one;
            return;
        }

        var root = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        root.name = "LobbyCharacter_Prototype";
        root.transform.position = new Vector3(0f, 1f, 0f);
        root.transform.localScale = new Vector3(0.65f, 1f, 0.65f);
        var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = new Color(0.12f, 0.35f, 0.58f);
        root.GetComponent<Renderer>().sharedMaterial = material;
        var label = new GameObject("CharacterLabel");
        label.transform.SetParent(root.transform);
        label.transform.localPosition = new Vector3(0f, 1.5f, 0f);
        var text = label.AddComponent<TextMesh>();
        text.text = "PERSONAGEM PROVISÓRIO";
        text.fontSize = 32;
        text.characterSize = 0.05f;
        text.anchor = TextAnchor.MiddleCenter;
    }

    private static void CreateCanvas()
    {
        var canvasGo = new GameObject("Stage1Canvas");
        canvasGo.AddComponent<Stage1LobbyController>();
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGo.AddComponent<CanvasScaler>();
        canvasGo.AddComponent<GraphicRaycaster>();
        var eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        var title = CreateText(canvasGo.transform, "BATTLE ROYALE", 34, new Vector2(0.5f, 0.92f), new Vector2(500, 70));
        title.color = new Color(0.8f, 0.92f, 1f);
        CreateText(canvasGo.transform, "ARSENAL    SKINS    PASSE    PERFIL    AMIGOS    CONFIGURAÇÕES", 18, new Vector2(0.5f, 0.83f), new Vector2(800, 45));
        CreateText(canvasGo.transform, "Créditos de Combate: 1000     Núcleos de Elite: 70", 20, new Vector2(0.5f, 0.12f), new Vector2(800, 50));
        CreateText(canvasGo.transform, "ETAPA 1 — LOBBY LOCAL", 16, new Vector2(0.5f, 0.06f), new Vector2(500, 35));
    }

    private static Text CreateText(Transform parent, string content, int size, Vector2 anchor, Vector2 dimensions)
    {
        var go = new GameObject(content.Replace(" ", "_"));
        go.transform.SetParent(parent, false);
        var rect = go.AddComponent<RectTransform>();
        rect.anchorMin = anchor; rect.anchorMax = anchor; rect.anchoredPosition = Vector2.zero; rect.sizeDelta = dimensions;
        var text = go.AddComponent<Text>();
        text.text = content; text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); text.fontSize = size;
        text.alignment = TextAnchor.MiddleCenter; text.color = Color.white;
        return text;
    }
}
