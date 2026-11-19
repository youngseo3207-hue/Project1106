using UnityEditor;
using UnityEngine;

public static class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    private static void FindInScene()
    {
        int count = 0;
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (EditorUtility.IsPersistent(go))
                continue;

            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing Script 발견: {go.name}", go);
                    count++;
                }
            }
        }

        Debug.Log($"씬 검사 완료. 발견된 Missing Script 수: {count}");
    }
}