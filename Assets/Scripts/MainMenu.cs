using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject music;
	public TextMeshProUGUI record, playText, version;
	public GameObject credits, about;

    // Start is called before the first frame update
    void Start()
    {
		if (PlayerPrefs.HasKey("climbed"))
		{
			record.text = PlayerPrefs.GetInt("climbed").ToString("#,#");
			playText.text = "Continue";
		}

		if (!FindObjectOfType<AudioSource>())
		{
			GameObject musicPlayer  = Instantiate(music);
			DontDestroyOnLoad(musicPlayer);
		}

		version.text = Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ToggleCredits()
	{
		credits.SetActive(!credits.activeSelf);
	}

	public void ToggleAbout()
	{
		about.SetActive(!about.activeSelf);
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void Photos()
	{
		SceneManager.LoadScene(2);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
