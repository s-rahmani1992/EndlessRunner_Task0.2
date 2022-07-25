using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehaviour : MonoBehaviour, IPoolable
{
    public static Vector2[] obstacleSizes = new Vector2[]{
        new Vector2(1.4f, 3.3f),
        new Vector2(2.6f, 2.1f),
        new Vector2(3.8f, 0.7f)
    };
    public string ObjectTag { get; set; }
    bool pass = false;
    [SerializeField]
    DynamicCube cube;
    public bool isFirst = false;
    
    public void OnPull(params object[] parameters){
        Vector4 v = (Vector4)parameters[0];
        transform.parent = LevelManager.Instance.transform;
        transform.position = new Vector4(v.x, 0, v.y);
        transform.localEulerAngles = new Vector3(0, v.w, 0);
        
        GetComponent<DynamicCube>().SetDimension(new Vector3(LevelManager.Instance.GroundWidth, 1, v.z));
        gameObject.SetActive(true);
    }

    public void OnPush(params object[] parameters){
        gameObject.SetActive(false);
        transform.parent = null;
    }


    public void PlaceObstacles(float offset){
        float start = 0;
        float r = offset + Random.Range(0, 10.0f);
        start += r;
        while(start < cube.Dimension.z - 15){
            ObjectPoolManager.Instance.PullFromList(1, start, obstacleSizes[Random.Range(0, 3)], transform);
            start += Random.Range(20, 30);
        }
    }
}
