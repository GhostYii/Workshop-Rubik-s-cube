//ORG: Ghostyii & MoonLight Game
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour 
{
    static private UIEvents instacne = null;
    static public UIEvents Instance { get { return instacne; } }

    private void Awake()
    {
        if (instacne == null)
            instacne = this;            
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
