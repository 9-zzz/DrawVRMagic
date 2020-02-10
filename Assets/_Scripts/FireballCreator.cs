using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCreator : MonoBehaviour
{
    public GameObject fireBall;
    GameObject fb;
    AccuracyDisplay ac;

    bool started = false;


    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<AccuracyDisplay>();
        
    }

    void ChargeBall(GameObject fb)
    {

    }

    IEnumerator WaitAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
            fb.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward *4, ForceMode.Impulse);
            fb.transform.parent = null;
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        print(ac.cc);

        if(ac.cc < 1 && started == false)
        {
            StartCoroutine(WaitAndDestroy(5));
            started = true;
            fb =  Instantiate(fireBall, transform.position, Quaternion.identity);
            fb.transform.localScale = Vector3.zero;
        }

        if(ac.cc == 0)
        {
            fb.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward *9, ForceMode.Impulse);
            fb.transform.parent = null;
            Destroy(gameObject);
        }

        if(fb != null)
        {
            //fb.transform.localScale = Vector3.Lerp(fb.transform.localScale, Vector3.one, 2*Time.deltaTime);
            fb.transform.localScale = new Vector3(1-ac.cc, 1-ac.cc, 1-ac.cc);
        }


        
    }
}
