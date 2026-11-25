#!/usr/bin/env bash
set -euo pipefail

PROJECT_PATH="$(cd "$(dirname "$0")" && pwd)"
UNITY_EDITOR="${UNITY_EDITOR:-}"

if [[ -z "$UNITY_EDITOR" ]]; then
  echo "Defina UNITY_EDITOR com o caminho do executável Unity 6000.3.29f1."
  echo 'Exemplo: export UNITY_EDITOR=/opt/Unity/Editor/Unity'
  exit 2
fi

if [[ ! -x "$UNITY_EDITOR" ]]; then
  echo "Executável Unity não encontrado ou sem permissão de execução: $UNITY_EDITOR"
  exit 2
fi

mkdir -p "$PROJECT_PATH/Builds/Android"
"$UNITY_EDITOR" \
  -projectPath "$PROJECT_PATH" \
  -buildTarget Android \
  -executeMethod BuildCommandLine.BuildAndroid \
  -batchmode \
  -nographics \
  -accept-apiupdate \
  -quit \
  -logFile "$PROJECT_PATH/Builds/Android/build.log"

if [[ ! -f "$PROJECT_PATH/Builds/Android/BattleRoyale-Stage1.apk" ]]; then
  echo "A Unity terminou, mas o APK não foi encontrado. Veja Builds/Android/build.log."
  exit 1
fi

echo "APK criado em: $PROJECT_PATH/Builds/Android/BattleRoyale-Stage1.apk"
