using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOnView : MonoBehaviour
{
    public Camera mainCamera;
    public float distanceInFront = 0.25f;
	public float waitTime;

    void Start()
    {
        StartCoroutine(PositionLevelRoutine(waitTime));
    }

    IEnumerator PositionLevelRoutine(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        Vector3 newPosition = mainCamera.transform.position;
        newPosition += new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z) * distanceInFront;
        Vector3 sumVector = new Vector3(0f, 0f, 0f);
        foreach (Transform child in gameObject.transform) {
            sumVector += child.position;
        }
        Vector3 groupCenter = sumVector / gameObject.transform.childCount;
        Vector3 groupOffset = groupCenter - transform.position;
		Debug.Log(groupOffset);
        //newPosition -= groupOffset;
        gameObject.transform.position = newPosition;
    }

    void Update()
    {
        
    }
}
