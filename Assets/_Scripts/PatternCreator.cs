using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCreator : MonoBehaviour
{
     public float distance = 50f;
     public GameObject pf;
     public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(inf(timer));

    }

    IEnumerator inf(float time)
    {
        yield return new WaitForSeconds(time);
        while(true)
        {
        yield return new WaitForSeconds(time);
         if(Input.GetMouseButton(0))
         {
              //create a ray cast and set it to the mouses cursor position in game
             Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast (ray, out hit, distance)) 
             {
                 //draw invisible ray cast/vector
                 Debug.DrawLine (ray.origin, hit.point);
                 Instantiate(pf , hit.point, Quaternion.identity);
                 //log hit area to the console
                 Debug.Log(hit.point);
                                   
             }    
             }    

        }
    }

     //replace Update method in your class with this one
     void Update () 
     {    
         //if mouse button (left hand side) pressed instantiate a raycast
     }

}
