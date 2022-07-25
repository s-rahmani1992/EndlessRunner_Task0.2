using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float GroundWidth { get; } = 5;
    public float speed = 20;
    [SerializeField]
    Text scoreTxt;
    [SerializeField]
    GameoverDialog dialog;
    int score = 0;
    public static LevelManager Instance { get; protected set; }
    DynamicCube[] segments;
    float[] directions;
    
    private void Awake(){
        Instance = this;
        segments = new DynamicCube[3];
        directions = new float[2];
    }

    private void Start(){
        IncrementScore(0);
        InputManager.Instance.enabled = true;
        float d = Random.Range(70.0f, 100.0f);
        segments[1] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(0, 0, d, 0)).GetComponent<DynamicCube>();
        segments[1].GetComponent<GroundBehaviour>().PlaceObstacles(50);
        Vector3 p;
        switch (Random.Range(0, 3)){
            case 0:
                p = segments[1].GetEnd() + (GroundWidth / 2) * (segments[1].transform.right + segments[1].transform.forward);
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y - 90)).GetComponent<DynamicCube>();
                directions[1] = -90;
                break;
            case 1:
                p = segments[1].GetEnd();
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y)).GetComponent<DynamicCube>();
                directions[1] = 0;
                break;
            case 2:
                p = segments[1].GetEnd() + (GroundWidth / 2) * (-segments[1].transform.right + segments[1].transform.forward);
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y + 90)).GetComponent<DynamicCube>();
                directions[1] = 90;
                break;
        }
        SpawnNextSegment();
    }

    private void Update(){
        if (segments[0] != null){
            segments[0].transform.position = new Vector3(Mathf.MoveTowards(segments[0].transform.position.x, 0, 1), 0, segments[0].transform.position.z);
            segments[0].transform.position -= speed * Time.deltaTime * new Vector3(0, 0, 1);
            if (segments[0].transform.position.z < -segments[0].Dimension.z - (GroundWidth / 2.0f)){
                StartCoroutine(Rotate());
            }
        }
    }

    private void OnDestroy(){
        Instance = null;
    }

    void SpawnNextSegment(){
        int g = Random.Range(0, 3);
        segments[1].transform.parent = transform;
        segments[2].transform.parent = transform;
        segments[0] = segments[1];
        segments[1] = segments[2];
        directions[0] = directions[1];
        Vector3 p;
        switch (g){
            case 0:
                p = segments[1].GetEnd() + (GroundWidth / 2) * (segments[1].transform.right + segments[1].transform.forward);
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y - 90)).GetComponent<DynamicCube>();
                directions[1] = -90;
                break;
            case 1:
                p = segments[1].GetEnd();
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y)).GetComponent<DynamicCube>();
                directions[1] = 0;
                break;
            case 2:
                p = segments[1].GetEnd() + (GroundWidth / 2) * (-segments[1].transform.right + segments[1].transform.forward);
                segments[2] = ObjectPoolManager.Instance.PullFromList(0, new Vector4(p.x, p.z, Random.Range(100.0f, 150.0f), segments[1].transform.localEulerAngles.y + 90)).GetComponent<DynamicCube>();
                directions[1] = 90;
                break;
        }
        segments[1].GetComponent<GroundBehaviour>().PlaceObstacles(20);
        segments[1].transform.parent = segments[2].transform.parent = segments[0].transform;
    }

    IEnumerator Rotate(){
        enabled = false;
        
        float speed = -500 * Mathf.Sign(directions[0]);
        float g = 0;
        float start = transform.localEulerAngles.y;
        while(Mathf.Abs(g) < Mathf.Abs(directions[0])){
            g += speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, start + g, 0);
            yield return null;
        }
        transform.localEulerAngles = new Vector3(0, start - directions[0], 0);
        DynamicCube f = segments[0];
        SpawnNextSegment();
        f.transform.parent = segments[0].transform;
        enabled = true;
        yield return new WaitForSeconds(2);
        ObjectPoolManager.Instance.Push2List(f.gameObject);
    }

    public void IncrementScore(int add){
        score += add;
        speed = Mathf.Clamp(5 * Mathf.Floor(score / 5.0f) + 10, 15, 50);
        scoreTxt.text = score.ToString();
    }

    public void Quit(){
        Application.Quit();
    }

    public void Reload(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void PopDialog(){
        speed = 0;
        int high = PlayerPrefs.GetInt("high");
        if (high < score)
            PlayerPrefs.SetInt("high", score);
        dialog.Popup(score, high < score);
    }
}
