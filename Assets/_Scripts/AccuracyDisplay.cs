using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyDisplay : MonoBehaviour
{
    public TextMesh text;
    public int originalChildCount;
    public float cc;

    public Transform targP;

    // Start is called before the first frame update
    void Start()
    {
        originalChildCount = targP.childCount;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.root.LookAt(Camera.main.transform);

        cc = ((float)targP.childCount/(float)originalChildCount);

        text.text = "ACC: " + ((1-cc)*100.0f).ToString("F2") + "%";

        text.color = new Color(cc,0.25f,(1.0f-cc));
        

    }
}
