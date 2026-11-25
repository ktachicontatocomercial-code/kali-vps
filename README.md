# Battle Royale — Etapa 1

Projeto inicial para **Unity 6.3.29f1 (6000.3.29f1)** com **URP**.

## Abrir no Codespace

1. Abra o repositório no GitHub.
2. Clique em **Code → Codespaces → Create codespace on master**.
3. No Codespace, abra a pasta como projeto Unity. Se o editor não aparecer no menu, use o caminho do projeto no comando de abertura da Unity.
4. Aguarde a Unity importar os pacotes.
5. No editor, execute **Battle Royale → Criar/Reconstruir Etapa 1**.
6. Abra a cena `Assets/Stage1/Scenes/Stage1_Lobby.unity` e pressione Play.

## O que já existe

- Estrutura de projeto Unity 6 + URP.
- Gerador de cena do lobby.
- Tela inicial para criar nome do jogador.
- Perfil local persistente em JSON.
- Lobby com personagem provisório, moedas e menus iniciais.
- Assets importáveis já dentro de `Assets/Stage1/Art/Characters`, `Assets/Stage1/Art/Animations` e `Assets/Stage1/Art/Weapons`.

## Observação sobre assets grandes

O pacote ZIP oficial completo de personagens, com aproximadamente 123 MB, não foi colocado no GitHub porque arquivos individuais acima de 100 MB são rejeitados sem Git LFS. O modelo humano oficial compatível já foi extraído e está dentro de `Assets/Stage1/Art/Characters/Superhero_Male_FullBody.fbx`.

## Teste rápido

No Play Mode, digite um nome na tela inicial e clique em **Continuar**. Feche e execute novamente para conferir que o perfil foi salvo localmente.

## Compilar APK sem abrir a interface da Unity

O projeto inclui `Assets/Stage1/Editor/BuildCommandLine.cs` e `build-android.sh`. No terminal do Codespace, estando na raiz do projeto e com a Unity 6000.3.29f1 instalada, execute:

```bash
export UNITY_EDITOR=/caminho/para/Unity/6000.3.29f1/Editor/Unity
chmod +x build-android.sh
./build-android.sh
```

O script usa `-batchmode`, `-nographics`, `-buildTarget Android`, `-executeMethod` e `-quit`. O resultado será salvo em `Builds/Android/BattleRoyale-Stage1.apk`.
