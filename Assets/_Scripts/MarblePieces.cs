using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePieces : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    Rigidbody rb;

    public bool canBeGrabbed = false;

    public float maxDistance = 20;

    public float distanceFromOrigin;
    public float scaleT;
    public float moveFactor = 0.001f;
    public float minDist;

    public Color originalColor;
    MeshRenderer mRend;

    public bool touched = false;
    public bool isback = false;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //parent = transform.root.gameObject;
        
        rb = GetComponent<Rigidbody>();

        mRend = GetComponent<MeshRenderer>();

        originalColor = mRend.materials[0].color;

        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

    }
    
    public void StartWait()
    {
        StartCoroutine(WaitAndSetGrabMode(2));
    }

    IEnumerator WaitAndSetGrabMode(float time)
    {
        print("this happened again");
        isback = false;
        yield return new WaitForSeconds(time);
        canBeGrabbed = true;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromOrigin = Vector3.Distance(transform.localPosition, originalPosition);
       
        if(canBeGrabbed)
        {
            rb.isKinematic = true;

            if (distanceFromOrigin < minDist || touched)
            {
                scaleT = 0.35f;
                mRend.materials[0].color = originalColor;
            }
            else
            {
                mRend.materials[0].color = Color.Lerp(originalColor, Color.red, Mathf.PingPong(Time.time, 1));

                scaleT = moveFactor * (1 - Mathf.Clamp(distanceFromOrigin / maxDistance, 0, 1));
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, scaleT);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalRotation, scaleT);

            if ((transform.localPosition == originalPosition) && (isback == false))
            {
                parent.GetComponent<MarbleController>().IncrementPiece();
                canBeGrabbed = false;
                isback = true;
            }
        }
    }
}
