///ORG: Ghostyii & MoonLight Game
using UnityEngine;

//Global config
public class GlobalConfig : MonoBehaviour
{
    static private GlobalConfig instance;

    static public GlobalConfig Instance
    { get { return instance; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //for game pause
    //游戏是否暂停
    public bool isPause = false;
    //can touch for rotation ?
    //旋转协程是否完成
    public bool enableRotate = true;
    [Range(0, 10)]
    public float cubeRotateSpeed = 2.5f;

    public Transform cubeRoot;
    public Transform rotationRoot;
}
