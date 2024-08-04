using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameManagerScript gameManager;
    public GameObject crosshair;

    void Start () 
    {
        //crosshair = FindObjectOfType<GameObject.crosshair>();
    }

void OnTriggerEnter(Collider other)
{ 
    if(other.tag == "Player") { 
    gameManager.CompleteLevel();
    crosshair.SetActive(false);
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}
}

}
