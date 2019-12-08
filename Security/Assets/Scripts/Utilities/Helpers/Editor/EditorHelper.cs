using UnityEditor;
using UnityEngine;

public static class EditorHelper
{
    [MenuItem("Tools/Generate Scene List", priority = 800)]
    public static void GenerateSceneList()
    {
        SceneListCheck.Generate();
    }

    [MenuItem("Tools/Delete PlayerPrefs", priority = 800)]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/Go to Persistant Data path folder", priority = 800)]
    public static void OpenPersistantDataPathFolder()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem("Tools/Go to Data path folder", priority = 800)]
    public static void OpenDataPathFolder()
    {
        EditorUtility.RevealInFinder(Application.dataPath);
    }

    [MenuItem("Tools/Remove Debug Objects", priority = 800)]
    public static void RemoveDebugObjects()
    {
        DebugHelper.RemoveAllDebugPositions();
    }
}
