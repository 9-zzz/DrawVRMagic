using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public AnimationCurve myCurve;
    public float speed = 10;
   
    void Update()
    {
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate(speed*(Time.time % myCurve.length)), transform.position.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
}
