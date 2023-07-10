using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject music;

    // Start is called before the first frame update
    void Start()
    {
		if (!FindObjectOfType<AudioSource>())
		{
			GameObject musicPlayer  = Instantiate(music);
			DontDestroyOnLoad(musicPlayer);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}
}
