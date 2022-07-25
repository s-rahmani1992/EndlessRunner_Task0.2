using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float s;
    // Start is called before the first frame update
    void Start()
    {
        s = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        s = Mathf.Clamp(s + 5* Time.deltaTime * InputManager.Instance.Delta, 0.4f, 3);
        transform.localScale = new Vector3(1, s, 1);
    }
}
