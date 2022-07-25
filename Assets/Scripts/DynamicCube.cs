using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DynamicCube : MonoBehaviour
{
    Mesh mesh;
    public Vector3 Dimension { get; protected set; } = new Vector3(1, 1, 1);

    private void Awake(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Start(){
        CalculateMeshData();
    }

    void CalculateMeshData(){
        mesh.vertices = new Vector3[] {
            new Vector3(Dimension.x/2, Dimension.y/2, 0), new Vector3(Dimension.x/2, -Dimension.y/2, 0), new Vector3(-Dimension.x/2, -Dimension.y/2, 0), new Vector3(-Dimension.x/2, Dimension.y/2, 0),
            new Vector3(Dimension.x/2, Dimension.y/2, 0), new Vector3(Dimension.x/2, -Dimension.y/2, 0), new Vector3(Dimension.x/2, -Dimension.y/2, Dimension.z), new Vector3(Dimension.x/2, Dimension.y/2, Dimension.z),
            new Vector3(-Dimension.x/2, -Dimension.y/2, 0), new Vector3(-Dimension.x/2, Dimension.y/2, 0), new Vector3(-Dimension.x/2, Dimension.y/2, Dimension.z), new Vector3(-Dimension.x/2, -Dimension.y/2, Dimension.z),
            new Vector3(Dimension.x/2, Dimension.y/2, 0), new Vector3(-Dimension.x/2, Dimension.y/2, 0), new Vector3(-Dimension.x/2, Dimension.y/2, Dimension.z), new Vector3(Dimension.x/2, Dimension.y/2, Dimension.z),
            new Vector3(Dimension.x/2, -Dimension.y/2, 0), new Vector3(-Dimension.x/2, -Dimension.y/2, 0), new Vector3(-Dimension.x/2, -Dimension.y/2, Dimension.z), new Vector3(Dimension.x/2, -Dimension.y/2, Dimension.z),
            new Vector3(Dimension.x/2, Dimension.y/2, Dimension.z), new Vector3(Dimension.x/2, -Dimension.y/2, Dimension.z), new Vector3(-Dimension.x/2, -Dimension.y/2, Dimension.z), new Vector3(-Dimension.x/2, Dimension.y/2, Dimension.z)
        };
        mesh.SetIndices(new int[] { 2, 3, 0, 1,
                                    7 ,6 ,5 , 4,
                                    11 ,10 ,9 , 8 ,
                                    12, 13, 14, 15,
                                    19, 18, 17, 16,
                                    23, 22, 21, 20}, MeshTopology.Quads, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    public void SetDimension(Vector3 dim){
        Dimension = dim;
        CalculateMeshData();
    }

    public Vector3 GetEnd(){
        return transform.position + Dimension.z * transform.forward;
    }
}
