using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Geometry : MonoBehaviour
{
    public Texture texture;

    [Range(0, 1)]
    public float _offsetX;
    [Range(0, 1)]
    public float _offsetY;
    public float offset = 0.065f;

    private Mesh mesh;
    private List<Vector3> verts;
    private List<Vector2> uv;
    private List<int> tris;

    // Start is called before the first frame update
    void Update()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
        mesh = new Mesh();
        verts = new List<Vector3>();
        tris = new List<int>();
        uv = new List<Vector2>();

        Cube();

        Debug.Log($"verts {verts.Count} | uv {uv.Count}");
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uv.ToArray();

        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Cube()
    {
        //front face
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(0, 1, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 1, 0));

        uv.Add(GetTexel(0 + _offsetX,0 + _offsetY));
        uv.Add(GetTexel(0 + _offsetX, offset + _offsetY));
        uv.Add(GetTexel(offset + _offsetX, 0 + _offsetY));
        uv.Add(GetTexel(offset + _offsetX, offset + _offsetY));

        tris.AddRange(new int[] { 0, 1, 2 });
        tris.AddRange(new int[] { 1, 3, 2 });

        //top face
        verts.Add(new Vector3(0, 1, 1));
        verts.Add(new Vector3(1, 1, 1));

        uv.Add(GetTexel(0, 1));
        uv.Add(GetTexel(1, 1));

        tris.AddRange(new int[] { 1, 4, 3 });
        tris.AddRange(new int[] { 4, 5, 3 });

        //left face 
        verts.Add(new Vector3(0, 0, 1));

        uv.Add(GetTexel(1, 1));

        tris.AddRange(new int[] {0, 6, 4 });
        tris.AddRange(new int[] {4, 1, 0 });
    }

    Vector2 GetTexel(float x, float y)
    {
        var xx = x + 0.5f/ texture.width;
        var yy = y + 0.5f/ texture.height;

        return new Vector2(xx, yy);
    }
}
