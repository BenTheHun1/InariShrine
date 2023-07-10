using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void Pause()
	{
		if (Time.timeScale == 1f)
		{
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
		}
		gameObject.SetActive(!gameObject.activeSelf);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.None;
	}
}
