using UnityEngine;
using UnityEditor;

public class CustomEditors : MonoBehaviour
{
    [CustomEditor(typeof(GameManager)), InitializeOnLoad]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Clear Player Storage"))
            {
                PlayerPrefs.DeleteAll();
                EventsPool.UpdateUI.Invoke();
                Debug.Log("Cleared all values!");
            }
            if (GUILayout.Button("GIVE ME SOME DAMN MONEEEYYYY"))
            {
                PlayerStorage.CoinsCollected = PlayerStorage.CoinsCollected + 100;
                EventsPool.UpdateUI.Invoke();
                Debug.Log("Greedy..");
            }
        }
    }
}
