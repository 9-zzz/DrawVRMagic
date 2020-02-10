using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbControl : MonoBehaviour
{
    public enum Hand { LEFT, RIGHT };

    public GameObject marble;
    public Hand hand = Hand.LEFT;
    private List<GameObject> objectsIn;
    private MarbleController marbleScript;

    void Start()
    {
        objectsIn = new List<GameObject>();
        marbleScript = marble.GetComponent<MarbleController>();
        if (hand == Hand.LEFT)
            Shader.SetGlobalFloat("_ORB_LEFT_SIZE", 0f);
        else
            Shader.SetGlobalFloat("_ORB_RIGHT_SIZE", 0f);
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject != marble && !other.gameObject.CompareTag("Orb") && objectsIn.Contains(other.gameObject)) {
            objectsIn.Remove(other.gameObject);
            // Reenable the base collision state of the platform once outside the orb
            BoxCollider col = other.gameObject.GetComponent<BoxCollider>();
            MeshCollider mcol = other.gameObject.GetComponent<MeshCollider>();
            Collider usecol = null;
            if (col != null)
                usecol = col;
            if (mcol != null)
                usecol = mcol;
            if (usecol != null) {
                // Default platform state
                Railing rail = other.gameObject.GetComponent<Railing>();
                if (rail != null) {
                    if (rail.platformType == Railing.RailType.NONSOLID)
                        usecol.isTrigger = true;
                    else
                        usecol.isTrigger = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject != marble && !other.gameObject.CompareTag("Orb") && !objectsIn.Contains(other.gameObject))
            objectsIn.Add(other.gameObject);
    }

    void Update() {
		/*
        if (hand == Hand.LEFT && Input.GetMouseButton(0)) {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + dist));
            Shader.SetGlobalVector("_ORB_LEFT_POS", gameObject.transform.position);
            Shader.SetGlobalFloat("_ORB_LEFT_SIZE", gameObject.transform.localScale.x / 2f);
        }
        else if (hand == Hand.RIGHT && Input.GetMouseButton(1)) {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + dist));
            Shader.SetGlobalVector("_ORB_RIGHT_POS", gameObject.transform.position);
            Shader.SetGlobalFloat("_ORB_RIGHT_SIZE", gameObject.transform.localScale.x / 2f);
        }
		*/
		if (hand == Hand.LEFT)
		{
			Shader.SetGlobalVector("_ORB_LEFT_POS", gameObject.transform.position);
			Shader.SetGlobalFloat("_ORB_LEFT_SIZE", gameObject.transform.lossyScale.x / 2f);
		}
		else if (hand == Hand.RIGHT)
		{
			Shader.SetGlobalVector("_ORB_RIGHT_POS", gameObject.transform.position);
			Shader.SetGlobalFloat("_ORB_RIGHT_SIZE", gameObject.transform.lossyScale.x / 2f);
		}

		for (int i = 0; i < objectsIn.Count; i++) {
            BoxCollider col = objectsIn[i].GetComponent<BoxCollider>();
            MeshCollider mcol = objectsIn[i].GetComponent<MeshCollider>();
            Collider usecol = null;
            if (col != null)
                usecol = col;
            if (mcol != null)
                usecol = mcol;
            if (usecol != null) {
                // Default platform state
                Railing rail = objectsIn[i].GetComponent<Railing>();
                if (rail != null) {
                    if (rail.platformType == Railing.RailType.NONSOLID)
                        usecol.isTrigger = true;
                    else
                        usecol.isTrigger = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        bool marbleInside = Vector3.Distance(marble.transform.position, gameObject.transform.position) <
                            Mathf.Abs(marble.transform.localScale.x / 2f - gameObject.transform.localScale.x / 2f);
        marbleInside = marbleInside && 
                       Mathf.Abs(marble.transform.position.y - gameObject.transform.position.y) <
                       Mathf.Abs(gameObject.transform.localScale.y / 2f);

        if (marbleInside) {
            // Marble is inside the orb
            for (int i = 0; i < marbleScript.objectsTouching.Count; i++) {
                if (objectsIn.Contains(marbleScript.objectsTouching[i])) {
                    BoxCollider col = marbleScript.objectsTouching[i].GetComponent<BoxCollider>();
                    MeshCollider mcol = marbleScript.objectsTouching[i].GetComponent<MeshCollider>();
                    Collider usecol = null;
                    if (col != null)
                        usecol = col;
                    if (mcol != null)
                        usecol = mcol;
                    if (usecol != null) {
                        // Swapped platform state
                        Railing rail = marbleScript.objectsTouching[i].GetComponent<Railing>();
                        if (rail != null) {
                            if (rail.platformType == Railing.RailType.NONSOLID)
                                usecol.isTrigger = false;
                            else if (rail.platformType == Railing.RailType.SOLID)
                                usecol.isTrigger = true;
                        }
                    }
                }
            }
        }
    }
}
