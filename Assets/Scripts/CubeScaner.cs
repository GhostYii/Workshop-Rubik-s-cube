///ORG: Ghostyii & MoonLight Game
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//scan cube by ray cast
//通过射线检测来扫描魔方的类
public class CubeScaner : MonoBehaviour
{
    //ray deepth
    //检测深度
    private int length = 8;

    static private CubeScaner instance = null;
    static public CubeScaner Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    //get cube group by face index
    //通过faceIndex来获取方块组
    public Cube[] ScanCubeGroupByFaceIndex(int index)
    {
        List<Cube> result = new List<Cube>();

        if (index <= 0 || index > 6) return result.ToArray();

        Vector3 origin = index > 3 ? new Vector3(2 * index - 10, 2, -4) : 
                                     new Vector3(4, 4 - 2 * index, -2) ;

        Vector3 dir = index > 3 ? Vector3.forward : Vector3.left;

        for (int i = 0; i < 3; i++)
        {
            Ray ray = new Ray(origin, dir);
            RaycastHit[] hits = Physics.RaycastAll(ray, length);
            foreach (RaycastHit hit in hits)
            {
                Cube c = hit.transform.gameObject.GetComponent<Cube>();
                if (!result.Contains(c))
                    result.Add(c);
            }
            if (index > 3) origin.y -= 2;
            else           origin.z += 2;
        }

        return result.ToArray();
    }

    //get face cube group
    //获取朝向玩家的那一面的所有角块
    public Cube[] ScanFaceGroup()
    {
        List<Cube> result = new List<Cube>();
        Vector3 origin = new Vector3(4, 2, -2);

        for (int i = 0; i < 3; i++)
        {
            Ray ray = new Ray(origin, Vector3.left);
            RaycastHit[] hits = Physics.RaycastAll(ray, length);
            foreach (RaycastHit hit in hits)
            {
                Cube c = hit.transform.gameObject.GetComponent<Cube>();
                if (!result.Contains(c))
                    result.Add(c);
            }
            origin.y -= 2;
        }        
        return result.ToArray();
    }

}
