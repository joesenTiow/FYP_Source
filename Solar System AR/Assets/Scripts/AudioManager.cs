using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager manager;

	public AudioSource clickSound, backSound;

	public AudioSource music;

	private void OnEnable()
	{
		if (manager == null)
		{
			manager = this;
		}
		else if (manager != this)
		{
			Destroy(manager.gameObject);
			manager = this;
		}
		DontDestroyOnLoad(gameObject);

		if (music == null)
		{
			music = GameObject.Find("Music").GetComponent<AudioSource>();
		}
		else if (music != GameObject.Find("Music").GetComponent<AudioSource>())
		{
			Destroy(music.gameObject);
			music = GameObject.Find("Music").GetComponent<AudioSource>();
		}
		DontDestroyOnLoad(music.gameObject);
	}

	// Start is called before the first frame update
	void Start()
	{
		music.loop = true;

		if (PlayerPrefs.GetString("ISMUSICON") == "True")
		{
			Debug.Log("music is on");
			music.mute = false;
		}
		else
		{
			Debug.Log("music is off");
			music.mute = true;
		}

		if (PlayerPrefs.GetString("ISSOUNDON") == "True")
		{
			clickSound.mute = false;
			backSound.mute = false;
		}
		else
		{
			clickSound.mute = true;
			backSound.mute = true;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ClickSound()
	{
		clickSound.Play();
	}

	public void BackSound()
	{
		backSound.Play();
	}

	public void OnToggleSoundOn()
	{
		clickSound.mute = false;
		backSound.mute = false;
		PlayerPrefs.SetString("ISSOUNDON", "True");
	}

	public void OnToggleSoundOff()
	{
		clickSound.mute = true;
		backSound.mute = true;
		PlayerPrefs.SetString("ISSOUNDON", "False");
	}

	public void OnToggleMusicOn()
	{
		music.mute = false;
		PlayerPrefs.SetString("ISMUSICON", "True");
	}

	public void OnToggleMusicOff()
	{
		music.mute = true;
		PlayerPrefs.SetString("ISMUSICON", "False");
	}
}
