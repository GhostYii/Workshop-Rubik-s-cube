///ORG: Ghostyii & MoonLight Game
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

//control the rotation of the whole cube
//控制方块组的旋转
public class CubeRotation : MonoBehaviour
{
    public List<Cube> cubes = new List<Cube>();

    //operations stack,reverse operation is actually stored
    //可撤销的操作栈，存储的是反向操作
    private Stack<Operating> moveDirs = new Stack<Operating>();
    public Stack<Operating> MoveDirs
    {
        get { return moveDirs; }
        set { moveDirs = value; }
    }

    static private CubeRotation instance = null;
    static public CubeRotation Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ResetRotation()
    {
        if (!GlobalConfig.Instance.enableRotate) return;

        StartCoroutine(ResetRotationRoutine(moveDirs));
    }
    public void ResetRotation(Stack<Operating> oStack)
    {
        if (!GlobalConfig.Instance.enableRotate) return;

        StartCoroutine(ResetRotationRoutine(oStack));
    }

    /*public IEnumerator ResetRotationRoutine()
    {
        GameObject go = null;
        while (MoveDirs.Count > 0)
        {
            RotationCoroutine script = null;
            go = new GameObject("RotationHandler");
            script = go.AddComponent<RotationCoroutine>();
            Operating o = MoveDirs.Pop();
            if (o.cubeGroupIndex == 0)
                script.BeginCoroutine(script.SetRotation(transform, o.moveDir, 12f));
            else
            {
                Cube[] cs = CubeScaner.Instance.ScanCubeGroupByIndex(o.cubeGroupIndex);
                foreach (Cube c in cs)
                    c.transform.SetParent(GlobalConfig.Instance.rotationRoot);
                script.BeginCoroutine(script.SetRotation(GlobalConfig.Instance.rotationRoot, cs, GlobalConfig.Instance.cubeRoot, o.moveDir));
            }

            yield return new WaitWhile(() => !GlobalConfig.Instance.enableRotate);
        }

        yield return null;
    }*/
    
    //the routine that control rotation
    //控制魔方整体旋转的协程
    public IEnumerator ResetRotationRoutine(Stack<Operating> oStack)
    {
        GameObject go = null;
        while (oStack.Count > 0)
        {
            RotationCoroutine script = null;
            go = new GameObject("RotationHandler");
            script = go.AddComponent<RotationCoroutine>();
            Operating o = oStack.Pop();
            if (o.cubeGroupIndex == 0)
                script.BeginCoroutine(script.SetRotation(transform, o.moveDir, 12f));
            else
            {
                Cube[] cs = CubeScaner.Instance.ScanCubeGroupByFaceIndex(o.cubeGroupIndex);
                foreach (Cube c in cs)
                    c.transform.SetParent(GlobalConfig.Instance.rotationRoot);
                script.BeginCoroutine(script.SetRotation(GlobalConfig.Instance.rotationRoot, cs, GlobalConfig.Instance.cubeRoot, o.moveDir));
            }

            yield return new WaitWhile(() => !GlobalConfig.Instance.enableRotate);
        }

        yield return null;
    }

    public void ResetAllCubeGroupIndex()
    {
        foreach (Cube c in cubes)
            c.faceIndex = 0;
    }

    public void ButtonRotation(int dir)
    {
        if (!GlobalConfig.Instance.enableRotate) return;

        MoveDirection d = (MoveDirection)dir;
        GameObject go = null;
        RotationCoroutine script = null;
        go = new GameObject("RotationHandler");
        script = go.AddComponent<RotationCoroutine>();

        MoveDirs.Push(new Operating(d.Reverse(), 0));

        script.BeginCoroutine(script.SetRotation(transform, d));
    }

    public void Undo()
    {
        if (!GlobalConfig.Instance.enableRotate) return;

        if (MoveDirs.Count <= 0) return;

        RotationCoroutine script = null;
        GameObject go = new GameObject("RotationHandler");
        script = go.AddComponent<RotationCoroutine>();

        Operating o = MoveDirs.Pop();
        if (o.cubeGroupIndex == 0)
            script.BeginCoroutine(script.SetRotation(transform, o.moveDir, 12f));
        else
        {
            Cube[] cs = CubeScaner.Instance.ScanCubeGroupByFaceIndex(o.cubeGroupIndex);
            foreach (Cube c in cs)
                c.transform.SetParent(GlobalConfig.Instance.rotationRoot);
            script.BeginCoroutine(script.SetRotation(GlobalConfig.Instance.rotationRoot, cs, GlobalConfig.Instance.cubeRoot, o.moveDir));
        }
    }

    //Random stack cannot undo
    //随机后不可撤销回初始状态
    public void RandomCube()
    {
        int time = Random.Range(10, 21);
        Stack<Operating> oStack = new Stack<Operating>();
        while(time-- > 0)
        {
            Operating o;
            o.cubeGroupIndex = Random.Range(0, 7);
            if (o.cubeGroupIndex == 0) o.moveDir = (MoveDirection)Random.Range(0, 7);
            else if (o.cubeGroupIndex >= 1 && o.cubeGroupIndex <= 3)
                o.moveDir = (MoveDirection)Random.Range(1, 3);
            else
                o.moveDir = (MoveDirection)Random.Range(3, 5); 

            oStack.Push(o);
        }

        ResetRotation(oStack);
    }

}

[System.Serializable]
public enum MoveDirection
{
    None,
    Left,
    Right,
    Up,
    Down,
}

[System.Serializable]
public struct Operating
{
    public MoveDirection moveDir;
    public int cubeGroupIndex;

    public Operating(MoveDirection md, int cgi)
    {
        moveDir = md;
        cubeGroupIndex = cgi;
    }
}