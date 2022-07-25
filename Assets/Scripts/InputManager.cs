using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static int refCount = 0;
    public float Delta { get; private set; }
    public static InputManager Instance{get; protected set;}

    private void Awake(){
        refCount++;
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else
            GameObject.DestroyImmediate(gameObject);
    }

    private void OnDestroy(){
        refCount--;
        if (refCount == 0)
            Instance = null;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_EDITOR_WIN
        if (Input.GetKey(KeyCode.UpArrow))
            Delta = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            Delta = -1;
        else
            Delta = 0;

#elif UNITY_ANDROID
        if(Input.touchCount > 0)
            Delta = 0.1f * Input.touches[0].deltaPosition.y;
#endif
    }
}