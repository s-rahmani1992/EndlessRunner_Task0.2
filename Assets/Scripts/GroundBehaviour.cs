using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehaviour : MonoBehaviour, IPoolable
{
    public string ObjectTag { get; set; }
    bool pass = false;
    [SerializeField]
    DynamicCube cube;
    public bool isFirst = false;
    
    public void OnPull(params object[] parameters)
    {
        Vector4 v = (Vector4)parameters[0];
        transform.parent = LevelManager.Instance.transform;
        transform.position = new Vector4(v.x, 0, v.y);
        transform.localEulerAngles = new Vector3(0, v.w, 0);
        
        GetComponent<DynamicCube>().SetDimension(new Vector3(4, 1, v.z));
        gameObject.SetActive(true);
    }

    public void OnPush(params object[] parameters)
    {
        gameObject.SetActive(false);
        transform.parent = null;
    }



}
