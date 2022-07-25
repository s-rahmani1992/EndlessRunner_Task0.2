using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyUtils
{
    public static string ToPersian(string num){
        System.Text.StringBuilder build = new System.Text.StringBuilder(num);
        for (int i = 0; i < num.Length; i++)
            build[i] = (char)(num[i] + 1728);
        return build.ToString();
    }

    public static IEnumerable<T> Shuffle<T>(IEnumerable<T> list){
        System.Random r = new System.Random();
        return list.OrderBy(x => r.Next());
    }

    public static int[] RandIndex(int n, int k){
        int[] h = new int[n];
        for (int i = 0; i < n; i++)
            h[i] = i;
        int[] rand = new int[k];
        System.Random r = new System.Random();
        h = h.OrderBy(x => r.Next()).ToArray();
        if (k == n)
            return h;
        for (int i = 0; i < k; i++)
            rand[i] = h[i];
        return rand;
    }

    public static int[] DerangeIndex(int n){
        int[] h = new int[n];
        for (int i = 0; i < n; i++)
            h[i] = i;
        int r = 0;
        int temp = 0;
        for(int i = 0; i < n - 1; i++)
        {
            r = Random.Range(i + 1, n);
            while(i == h[r])
                r = Random.Range(i + 1, n);
            temp = h[i];
            h[i] = h[r];
            h[r] = temp;
        }
        return h;
    }

    public static int[] RandIndexWithDestribute(int[] distribution){
        int s = 0;
        foreach (int a in distribution)
            s += a;
        int[] indices = new int[s];
        int tempIndex = 0;
        for (int i = 0; i < distribution.Length; i++){
            for (int j = 0; j < distribution[i]; j++){
                indices[tempIndex] = i;
                tempIndex++;
            }
        }

        System.Random r = new System.Random();
        indices = indices.OrderBy(x => r.Next()).ToArray();
        return indices;
    }

    public static void DelayAction(System.Action func, float delay, MonoBehaviour mono){
        mono.StartCoroutine(DelayAct(func, delay));
    }

    public static void DelayLoadScene(float delay, string name, MonoBehaviour mono){
        mono.StartCoroutine(DelayScene(delay, name));
    }

    static IEnumerator DelayAct(System.Action func, float delay){
        yield return new WaitForSeconds(delay);
        func?.Invoke();
    }

    static IEnumerator DelayScene(float delay, string name){
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}