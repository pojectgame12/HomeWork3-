using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameMenuUI : MonoBehaviour
{
    public enum MenuType { Main, Victory, GameOver }
    public MenuType menuType;
    private void OnGUI()
    {
        float s = Mathf.Min(Screen.width / 1280f, Screen.height / 720f);
        GUI.matrix = Matrix4x4.TRS(new Vector3((Screen.width-1280*s)*0.5f,(Screen.height-720*s)*0.5f,0),Quaternion.identity,new Vector3(s,s,1));
        GUI.Box(new Rect(270,105,740,500),"");
        var title = new GUIStyle(GUI.skin.label) {fontSize=52,fontStyle=FontStyle.Bold,alignment=TextAnchor.MiddleCenter};
        title.normal.textColor=new Color(.35f,.94f,.90f);
        var body = new GUIStyle(GUI.skin.label) {fontSize=23,alignment=TextAnchor.MiddleCenter,wordWrap=true};
        body.normal.textColor=Color.white;
        var button = new GUIStyle(GUI.skin.button) {fontSize=26,fontStyle=FontStyle.Bold};
        GameSession data = GameSession.Instance;
        if (menuType == MenuType.Main)
        {
            GUI.Label(new Rect(320,135,640,85),"SHADOW SWITCH",title);
            GUI.Label(new Rect(340,225,600,95),"LEVEL 2  •  FADING LIGHT\nCreate a solid shadow bridge. Save your lamp energy.",body);
            if (GUI.Button(new Rect(460,342,360,70),"PLAY LEVEL 2",button)) SceneManager.LoadScene("Level02");
            GUI.Label(new Rect(380,443,520,100),"Desktop: A/D, SPACE, E, J/L, R\nMobile: touch the on-screen buttons",body);
        }
        else if (menuType == MenuType.Victory)
        {
            GUI.Label(new Rect(320,130,640,85),"LEVEL COMPLETE!",title);
            GUI.Label(new Rect(320,225,640,110),"Crystals: " + data.crystalsCollected + "     Energy left: " + data.remainingEnergy.ToString("0") + "%\nTime: " + data.elapsedSeconds.ToString("0.0") + " s   •   Attempts: " + data.totalAttempts,body);
            if (GUI.Button(new Rect(470,350,340,65),"PLAY AGAIN",button)) SceneManager.LoadScene("Level02");
            if (GUI.Button(new Rect(470,442,340,65),"MAIN MENU",button)) SceneManager.LoadScene("MainMenu");
        }
        else
        {
            GUI.Label(new Rect(320,130,640,85),"GAME OVER",title);
            GUI.Label(new Rect(320,225,640,95),"You fell into the pit.\nAim the light before crossing the bridge!",body);
            if (GUI.Button(new Rect(470,350,340,65),"RETRY",button)) SceneManager.LoadScene("Level02");
            if (GUI.Button(new Rect(470,442,340,65),"MAIN MENU",button)) SceneManager.LoadScene("MainMenu");
        }
        GUI.matrix=Matrix4x4.identity;
    }
}
