using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionEnter(Collision other) {
        MarbleController mar = other.gameObject.GetComponent<MarbleController>();
        if (mar != null) {
            MarbleController.audSource.PlayOneShot(Resources.Load<AudioClip>("Audio/level-complete"));
            SceneManager.LoadScene(nextScene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
