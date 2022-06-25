using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionVFX;
    PlayerControls playerControls;
    MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        playerControls = GetComponent<PlayerControls>();
    }

    void OnTriggerEnter(Collider other)
    {
        explosionVFX.Play();
        Debug.Log("Triggered");
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        meshRenderer.enabled = false;
        playerControls.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
