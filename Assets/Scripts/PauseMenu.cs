using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public Slider volume, sense;

	public AudioSource[] audios;

	public CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
		gameObject.SetActive(false);

		audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

		if (PlayerPrefs.HasKey("vol"))
		{
			volume.value = PlayerPrefs.GetFloat("vol");
		}
		if (PlayerPrefs.HasKey("sense"))
		{
			sense.value = PlayerPrefs.GetFloat("sense");
		}
		SetSensitivity();
		SetVolume();
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
		PlayerPrefs.SetInt("climbed", FindObjectOfType<StairsController>().stairsClimbed);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.None;
		SceneManager.LoadScene(0);
	}

	public void SetVolume()
	{
		foreach (AudioSource source in audios)
		{
			source.volume = volume.value;
		}
		PlayerPrefs.SetFloat("vol", volume.value);
	}

	public void SetSensitivity()
	{
		cam.mouseSensitivity = sense.value;
		PlayerPrefs.SetFloat("sense", sense.value);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
