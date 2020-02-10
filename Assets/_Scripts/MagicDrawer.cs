using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDrawer : MonoBehaviour
{
    private List<GameObject> colliders = new List<GameObject>();
    public List<GameObject> GetColliders() { return colliders; }

    public string grabButton;
    public string otherGrab;
    public Transform parentCube;
    public Transform otherHand;
    public GameObject drawsphere;

bool biggen = false;
public Vector3 sizeHand;
public Vector3 origSizeHand;
public float smoothingSize = 7.0f;

public float dsForce = 3.0f;
public float lifeTime = 6.0f;

public ParticleSystem sparks;

public List<GameObject> tempList;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(!colliders.Contains(other.gameObject)) { colliders.Add(other.gameObject); }

        if (other.gameObject.GetComponent<MarblePieces>())
        {
            other.gameObject.GetComponent<MarblePieces>().touched = true;
            print("hello");
            //go.GetComponent<MarblePieces>().enabled = false;
            //go.GetComponent<MarblePieces>().scaleT = 0.35f;
            //other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - other.gameObject.GetComponent<MarblePieces>().originalPosition)*5, ForceMode.Impulse);

        } */
    }

    private void OnTriggerExit(Collider other)
    {
        //colliders.Remove(other.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator ScaleHand(float time)
    {
        biggen = true;
        yield return new WaitForSeconds(time);
        biggen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(biggen)
        {
            otherHand.localScale = Vector3.Lerp(otherHand.localScale, sizeHand, Time.deltaTime * smoothingSize);
        }
        else{

            otherHand.localScale = Vector3.Lerp(otherHand.localScale, origSizeHand, Time.deltaTime * smoothingSize);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton5)|| Input.GetMouseButtonDown(0))
            sparks.Play();

        if (Input.GetKeyUp(KeyCode.JoystickButton5) || Input.GetMouseButtonUp(0))
            sparks.Stop();

        if (Input.GetKey(KeyCode.JoystickButton5) || Input.GetMouseButton(0))
        {
            var ds = Instantiate(drawsphere, transform.position, Quaternion.identity) as GameObject;

            ds.transform.SetParent(parentCube);

        }// end of grab button

        if (Input.GetButtonDown(otherGrab)|| Input.GetMouseButtonDown(1))
        {
            foreach(Transform ds in parentCube.transform)
            {
                tempList.Add(ds.transform.gameObject);
            }

            foreach(GameObject ds in tempList)
            {
                ds.transform.SetParent(null);
                ds.GetComponent<Rigidbody>().isKinematic = false;
                ds.GetComponent<Rigidbody>().AddForce(parentCube.transform.forward * dsForce, ForceMode.Impulse);
                Destroy(ds.gameObject, lifeTime);
            }

            tempList.Clear();

            StartCoroutine(ScaleHand(0.24f));
        }

    }


}
