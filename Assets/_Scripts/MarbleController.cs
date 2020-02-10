using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarbleController : MonoBehaviour
{
    public List<GameObject> objectsTouching;
    private float ticker = 0f;
    private float deathTimer = 0f;

    public GameObject marblePiecesGroup;

    public Renderer mRend;
    public Rigidbody mRb;
    public float hitforce;

    public int pieceCount = 0;

    public ParticleSystem shine;

    public static AudioSource audSource;

    public void IncrementPiece()
    {
        pieceCount++;

        if(pieceCount == 5)
        {
            mRb.isKinematic = false;
            mRend.enabled = true;
            marblePiecesGroup.SetActive(false);

            pieceCount = 0;
            shine.Play();
            audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/successful-reconstruction"));
        }
        else {
            float random = Random.value;
            if (random < 0.3)
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/snap-into-place-3"));
            else if (random < 0.67)
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/snap-into-place-1"));
            else
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/snap-into-place-2"));
        }
    }

    void Start()
    {
        audSource = gameObject.AddComponent<AudioSource>();
        mRend = GetComponent<MeshRenderer>();
        mRb = GetComponent<Rigidbody>();
        objectsTouching = new List<GameObject>();
    }

    void FixedUpdate() {
        ticker += 1f;
        Shader.SetGlobalFloat("_TICKER", ticker);
    }

    void OnCollisionExit(Collision other) {
        if (objectsTouching.Contains(other.gameObject)) {
            BoxCollider col = other.gameObject.GetComponent<BoxCollider>();
            MeshCollider mcol = other.gameObject.GetComponent<MeshCollider>();
            if ((col == null && mcol == null) || (col != null && !col.isTrigger) || (mcol != null && !mcol.isTrigger))
                objectsTouching.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "hitred") {
            deathTimer = 0f;
        }
    }

    void OnCollisionEnter(Collision other) {
        if (!objectsTouching.Contains(other.gameObject)) {
            float random = Random.value;
            if (random < 0.3)
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/impact-floor-3"));
            else if (random < 0.67)
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/impact-floor-1"));
            else
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/impact-floor-2"));
            objectsTouching.Add(other.gameObject);
        }
        
        if (other.gameObject.tag == "hitred")
        {
            deathTimer += 1;
            if (deathTimer >= 3) {
                audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/shatter"));
                Destroy(other.gameObject);
                // start countdown 
                mRb.isKinematic = true;
                mRend.enabled = false;

                marblePiecesGroup.SetActive(true);
                var contact = other.contacts[0];

                foreach (Transform mrb in marblePiecesGroup.transform) {
                    mrb.GetComponent<Rigidbody>().AddForce((contact.point - mrb.transform.position) * hitforce, ForceMode.Impulse);
                    mrb.GetComponent<Rigidbody>().isKinematic = false;
                    mrb.GetComponent<MarblePieces>().StartWait();
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (!other.gameObject.CompareTag("Orb") && objectsTouching.Contains(other.gameObject)) {
            BoxCollider col = other.gameObject.GetComponent<BoxCollider>();
            MeshCollider mcol = other.gameObject.GetComponent<MeshCollider>();
            if ((col == null && mcol == null) || (col != null && !col.isTrigger) || (mcol != null && !mcol.isTrigger))
                objectsTouching.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "hitred") {
            deathTimer = 0f;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Orb") && !objectsTouching.Contains(other.gameObject))
            objectsTouching.Add(other.gameObject);
        if (other.gameObject.tag == "hitred") {
            deathTimer = 0f;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("AnalogRightClick") || Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
