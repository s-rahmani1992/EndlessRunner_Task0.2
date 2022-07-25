using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolable
{
    public string ObjectTag { get; set; }

    public void OnPull(params object[] parameters){
        transform.parent = parameters[2] as Transform;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = new Vector3(0, 0.5f, (float)parameters[0]);
        Vector2 vec = (Vector2)parameters[1];
        transform.GetChild(0).localScale = transform.GetChild(1).localScale = new Vector3(0.5f, vec.y, 0.5f);
        transform.GetChild(0).localPosition = new Vector3(vec.x / 2, vec.y / 2, 0);
        transform.GetChild(1).localPosition = new Vector3(-vec.x / 2, vec.y / 2, 0);
        transform.GetChild(2).localScale = new Vector3(vec.x + 0.5f, 0.5f, 0.5f);
        transform.GetChild(2).localPosition = new Vector3(0, vec.y + 0.25f, 0);
        gameObject.SetActive(true);
    }

    public void OnPush(params object[] parameters){
        transform.parent = null;
        gameObject.SetActive(false);
    }
}
