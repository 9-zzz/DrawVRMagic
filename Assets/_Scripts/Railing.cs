using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railing : MonoBehaviour
{
    public enum RailType { NONSOLID, SOLID, PERMA_SOLID };
    public RailType platformType;
    public Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        MeshCollider mcol = gameObject.GetComponent<MeshCollider>();
        Collider usecol = null;
        vertices = new Vector3[0];
        if (col != null)
            usecol = col;
        if (mcol != null) {
            usecol = mcol;
        }
        Material mat = gameObject.GetComponent<Renderer>().material;
        platformType = RailType.PERMA_SOLID;
        if (mat != null) {
            if (mat.name.Contains("Red"))
                platformType = RailType.SOLID;
            if (mat.name.Contains("Green"))
                platformType = RailType.NONSOLID;
        }
        if (usecol != null) {
            if (platformType == RailType.NONSOLID)
                usecol.isTrigger = true;
            else
                usecol.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MeshCollider mcol = gameObject.GetComponent<MeshCollider>();
        if (mcol != null) {
            vertices = mcol.sharedMesh.vertices;
        }
    }

    bool IntersectsVertices(Vector3 point, float radius) {
        foreach (Vector3 vert in vertices) {
            if (Vector3.Distance(point, vert) <= radius)
                return true;
        }
        return false;
    }
}
