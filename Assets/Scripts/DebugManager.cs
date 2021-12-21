using System.Text;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour {

    // Editor Fields
    public TextMeshProUGUI textDebug;

    // Variables
    private StringBuilder logStringBuilder = new StringBuilder();

    // Unity Messages
    private void OnEnable() {
        SetEvents();
    }

    private void OnDisable() {
        ClearEvents();
    }

    // Event Methods
    private void SetEvents() {
        Application.logMessageReceived += OnLogMessage;
    }

    private void ClearEvents() {
        Application.logMessageReceived -= OnLogMessage;
    }

    private void OnLogMessage(string pCondition, string pStackTrace, LogType pLogType) {
        logStringBuilder.Append(pCondition + "\n");
        textDebug.text = logStringBuilder.ToString();
    }
}
