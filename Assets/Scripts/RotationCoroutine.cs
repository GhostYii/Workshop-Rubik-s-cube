///ORG: Ghostyii & MoonLight Game
using UnityEngine;
using System.Collections;

public class RotationCoroutine : MonoBehaviour
{
    public void BeginCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public IEnumerator SetRotation(Transform target, MoveDirection dir, float fadeSpeed = 8f)
    {
        transform.position = target.position;
        transform.localRotation = target.localRotation;
        transform.localScale = target.localScale;

        //Debug.Break();
        Quaternion rot = Quaternion.identity;

        switch (dir)
        {
            case MoveDirection.None:
                break;
            case MoveDirection.Left:
                transform.RotateAround(transform.position, Vector3.up, 90f);
                break;
            case MoveDirection.Right:
                transform.RotateAround(transform.position, Vector3.down, 90f);
                break;
            case MoveDirection.Up:
                transform.RotateAround(transform.position, Vector3.right, 90f);
                break;
            case MoveDirection.Down:
                transform.RotateAround(transform.position, Vector3.left, 90f);
                break;
            default:
                break;
        }

        rot = transform.rotation;


        while (Quaternion.Angle(target.rotation, rot) > 1f)
        {

            GlobalConfig.Instance.enableRotate = false;
            target.rotation = Quaternion.Slerp(target.rotation, rot, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }

        target.rotation = rot;
        GlobalConfig.Instance.enableRotate = true;

        Destroy(gameObject);
        yield return null;
    }
    //set cube group rotation
    //控制方块组的旋转协程
    public IEnumerator SetRotation(Transform target, Cube[] cs,Transform r, MoveDirection dir, float fadeSpeed = 8f)
    {
        transform.position = target.position;
        transform.localRotation = target.localRotation;
        transform.localScale = target.localScale;

        //Debug.Break();
        Quaternion rot = Quaternion.identity;

        switch (dir)
        {
            case MoveDirection.None:
                break;
            case MoveDirection.Left:
                transform.RotateAround(transform.position, Vector3.up, 90f);
                break;
            case MoveDirection.Right:
                transform.RotateAround(transform.position, Vector3.down, 90f);
                break;
            case MoveDirection.Up:
                transform.RotateAround(transform.position, Vector3.right, 90f);
                break;
            case MoveDirection.Down:
                transform.RotateAround(transform.position, Vector3.left, 90f);
                break;
            default:
                break;
        }

        rot = transform.rotation;


        while (Quaternion.Angle(target.rotation, rot) > 1f)
        {

            GlobalConfig.Instance.enableRotate = false;
            target.rotation = Quaternion.Slerp(target.rotation, rot, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }

        target.rotation = rot;
        GlobalConfig.Instance.enableRotate = true;

        foreach (Cube c in cs)
            c.transform.SetParent(r);

        Destroy(gameObject);
        yield return null;
    }

}
