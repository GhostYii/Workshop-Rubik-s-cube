///ORG: Ghostyii & MoonLight Game
using UnityEngine;

//Control block
public class Cube : MonoBehaviour
{
    //the number of faces facing the player,examples are as follows.
    //面向玩家的那个面的编号，示例如下
    public int faceIndex = 0;
    //3 2 1
    //6 5 4
    //9 8 7

    static public MoveDirection moveDir = MoveDirection.None;

    private Vector3 downPos = Vector3.zero;
    private Vector3 upPos = Vector3.zero;

    private void OnMouseDown()
    {        
        if (!GlobalConfig.Instance.enableRotate) return;
        
        Cube[] cs = CubeScaner.Instance.ScanFaceGroup();
        for (int i = 0; i < cs.Length; i++)
            cs[i].faceIndex = i + 1;
        //Debug.Log(cubeGroupIndex);

        moveDir = MoveDirection.None;
        downPos = Input.mousePosition;
        upPos = downPos;        
    }

    private void OnMouseUp()
    {
        if (!GlobalConfig.Instance.enableRotate) return;

        upPos = Input.mousePosition;
        moveDir = GetDirection();

        int index = GetCubeGroupIndex();
        RotationCoroutine script = null;
        GameObject go = new GameObject("RotationHandler");
        script = go.AddComponent<RotationCoroutine>();

        CubeRotation.Instance.MoveDirs.Push(new Operating(moveDir.Reverse(), index));
        Cube[] cs = CubeScaner.Instance.ScanCubeGroupByFaceIndex(index);
        foreach (Cube c in cs)
            c.transform.SetParent(GlobalConfig.Instance.rotationRoot);

        script.BeginCoroutine(script.SetRotation(GlobalConfig.Instance.rotationRoot, cs, GlobalConfig.Instance.cubeRoot, moveDir));
        CubeRotation.Instance.ResetAllCubeGroupIndex();
    }

    //gets the direction in which the player moves the mouse
    //获取移动方向
    public MoveDirection GetDirection()
    {
        float xOffset = upPos.x - downPos.x;
        float yOffset = upPos.y - downPos.y;

        if (xOffset < -80 && yOffset < 40)
            return MoveDirection.Left;

        if (xOffset > 80 && yOffset < 40)
            return MoveDirection.Right;

        if (yOffset < -80 && xOffset < 40)
            return MoveDirection.Down;

        if (yOffset > 80 && xOffset < 40)
            return MoveDirection.Up;

        return MoveDirection.None;

    }

    //get the cube group according to the player clicking the cube and moving direction
    //根据玩家所点击的角块与移动方向确定旋转的方块组
    //view the cubegroup.png to get it
    //方块组图示参阅工程中的cubegroup.png文件
    public int GetCubeGroupIndex()
    {
        switch (moveDir)
        {
            case MoveDirection.Left:
                if (faceIndex >= 1 && faceIndex <= 3)
                    return 1;
                else if (faceIndex >= 4 && faceIndex <= 6)
                    return 2;
                else if (faceIndex >= 7 && faceIndex <= 9)
                    return 3;
                else break;
            case MoveDirection.Right:
                if (faceIndex >= 1 && faceIndex <= 3)
                    return 1;
                else if (faceIndex >= 4 && faceIndex <= 6)
                    return 2;
                else if (faceIndex >= 7 && faceIndex <= 9)
                    return 3;
                else break;
            case MoveDirection.Up:
                if (faceIndex == 1 || faceIndex == 4 || faceIndex == 7)
                    return 6;
                else if (faceIndex == 2 || faceIndex == 5 || faceIndex == 8)
                    return 5;
                else if (faceIndex == 3 || faceIndex == 6 || faceIndex == 9)
                    return 4;
                else break;
            case MoveDirection.Down:
                if (faceIndex == 1 || faceIndex == 4 || faceIndex == 7)
                    return 6;
                else if (faceIndex == 2 || faceIndex == 5 || faceIndex == 8)
                    return 5;
                else if (faceIndex == 3 || faceIndex == 6 || faceIndex == 9)
                    return 4;
                else break;
            default:
                break;
        }
        return 0;
    }

}
