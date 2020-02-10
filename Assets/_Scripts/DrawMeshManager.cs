using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMeshManager : MonoBehaviour
{
    public Material lMat;
    public GameObject trackedObj; // has to be steamvr obj?

    [SerializeField]
    private MeshLineRenderer currLine;

    public int numOfClicks = 0;

    public string drawButton;
    public float minStartWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(drawButton) || Input.GetMouseButtonDown(0))
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currLine = go.AddComponent<MeshLineRenderer>();

            currLine.lmat = new Material(lMat);
            currLine.setWidth(minStartWidth);
            numOfClicks = 0;

        }
        else if (Input.GetButton(drawButton) || Input.GetMouseButton(0))
        {
            currLine.AddPoint(trackedObj.transform.position);
            numOfClicks++;

        }
        else if (Input.GetButtonUp(drawButton) || Input.GetMouseButtonUp(0))
        {
            numOfClicks = 0;
            currLine = null;
        }

        if( currLine != null)
        {
            currLine.lmat.color = Color.blue;
        }

    }
}
