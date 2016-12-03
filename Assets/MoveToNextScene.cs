using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MoveToNextScene : MonoBehaviour {

    int currentScene;
    // Use this for initialization
    void Start () {
		currentScene = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void loadNextScene()
    {
        currentScene += 1;
        SceneManager.LoadScene(currentScene);
    }
}
