using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Editor
{
  public class SimpleTreeMenuEditor<T> where T : ScriptableObject
  {
    private readonly string _itemDataPath;
    protected readonly OdinMenuEditorWindow Parent;

    public SimpleTreeMenuEditor(string itemDataPath, OdinMenuEditorWindow parent)
    {
      _itemDataPath = itemDataPath;
      Parent = parent;
    }

    public OdinMenuTree BuildMenuTree(string header)
    {
      var tree = new OdinMenuTree(supportsMultiSelect: true);
      tree.Config.DrawSearchToolbar = true;

      tree.Add(header, null);
      tree.AddAllAssetsAtPath(header, _itemDataPath, typeof(T), includeSubDirectories: true);

      return tree;
    }

    public void OnBeginDrawEditors(OdinMenuTree menuTree)
    {
      OdinMenuItem selected = menuTree.Selection.FirstOrDefault();
      int toolbarHeight = menuTree.Config.SearchToolbarHeight;

      SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
      {
        if (selected != null)
          GUILayout.Label(selected.Name);

        GameEditorExtensions.DrawDeleteButton<T>(selected);
        GameEditorExtensions.DrawForceReSaveButton<T>(menuTree);
      }
      SirenixEditorGUI.EndHorizontalToolbar();
    }

    protected virtual void OnNewCreated(T design) =>
      Parent.TrySelectMenuItemWithObject(design);
  }
}