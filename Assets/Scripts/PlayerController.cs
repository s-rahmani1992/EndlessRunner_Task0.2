using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float s;

    void Start(){
        s = transform.localScale.y;
    }

    void Update(){
        s = Mathf.Clamp(s + 5* Time.deltaTime * InputManager.Instance.Delta, 0.4f, 3);
        transform.parent.localScale = new Vector3(1, s, 1);
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("death")){
            InputManager.Instance.enabled = false;
            LevelManager.Instance.PopDialog();
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("pass")){
            LevelManager.Instance.IncrementScore(1);
            MyUtils.DelayAction(() =>{
                ObjectPoolManager.Instance.Push2List(other.transform.parent.gameObject);
            }, 2, this);
        }
    }
}
