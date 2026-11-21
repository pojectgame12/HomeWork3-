using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelController : MonoBehaviour
{
    private void Start() { GameSession.Instance.BeginLevel(); }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("Level02");
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");
    }
    private void OnGUI()
    {
        var label = new GUIStyle(GUI.skin.label) {fontSize = Mathf.Max(17, Mathf.RoundToInt(Screen.height * .031f)), fontStyle = FontStyle.Bold};
        label.normal.textColor = new Color(.8f,1f,1f);
        float energy = LampController.Instance != null ? LampController.Instance.Energy : 60f;
        GUI.Box(new Rect(12,12,Mathf.Min(740,Screen.width-24),95), "");
        GUI.Label(new Rect(27,14,690,40), "SHADOW SWITCH  •  LEVEL 2: FADING LIGHT",label);
        GUI.Label(new Rect(27,49,690,40), "ENERGY: " + Mathf.RoundToInt(energy) + "%   •   CRYSTAL: " + GameSession.Instance.crystalsCollected + "/1",label);
        var helper = new GUIStyle(GUI.skin.box) {fontSize = Mathf.Max(13,Mathf.RoundToInt(Screen.height * .021f)),alignment=TextAnchor.MiddleCenter};
        string text = "A/D: move  •  SPACE: jump  •  E: lamp  •  J/L or mouse: aim  •  R: retry";
        GUI.Box(new Rect(Mathf.Max(5,(Screen.width-750)/2),Screen.height - 58,Mathf.Min(750,Screen.width-10),40),text,helper);
        if (energy <= 0 && GameSession.Instance.crystalsCollected > 0)
            GUI.Label(new Rect(Screen.width*.4f,95,Screen.width*.4f,40),"No energy: press R to retry", label);
    }
}
