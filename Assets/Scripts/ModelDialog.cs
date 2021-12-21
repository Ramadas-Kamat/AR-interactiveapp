using UnityEngine;
using TMPro;

public class ModelDialog : MonoBehaviour {

    // Editor Fields
    public TextMeshProUGUI text;

    // Variables
    [HideInInspector]
    public bool isVisible = true;

    // Unity Messages
    private void Start() {
        SetVisibility(false);
    }

    // Methods
    public void SetVisibility(bool pIsVisible) {
        gameObject.SetActive(pIsVisible);
        isVisible = pIsVisible;
    }

    public void Set(string pMessage) {
        SetVisibility(true);
        text.text = pMessage;
    }

    public void OnClose() {
        SetVisibility(false);
    }
}
