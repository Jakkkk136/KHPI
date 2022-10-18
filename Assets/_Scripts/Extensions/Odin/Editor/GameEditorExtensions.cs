using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
  public static class GameEditorExtensions
  {
    public static void DrawDeleteButton<TStaticData>(OdinMenuItem selected) where TStaticData : Object
    {
      if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete")))
      {
        if (selected.Value is TStaticData design)
          AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(design));
      }
    }

    public static void DrawForceReSaveButton<TStaticData>(OdinMenuTree menuTree) where TStaticData : Object
    {
      if (SirenixEditorGUI.ToolbarButton(new GUIContent("Re-Serialize All")))
      {
        IEnumerable<string> paths = menuTree
          .EnumerateTree()
          .Where(x => x.Value is Object)
          .Select(x => AssetDatabase.GetAssetPath(x.Value as Object));

        AssetDatabase.ForceReserializeAssets(paths);
      }
    }
  }
}