using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LearnManager : MonoBehaviour
{
	public int topicNumber = 0;

	public Button backButton;
	public Button finishButton;
	public Button infoButton;
	public Button nextButton;
	public Button previousButton;
	public Button rotateButton;
	public Button soundButton;
	public Button stopRotateButton;

	public GameObject infoPanel;
	public GameObject successPanel;
	public GameObject loginMessagePanel;
	public GameObject virtualObject;
	public GameObject[] virtualObjects;

	public Text lessonNameText;
	public Text text1;
	public Text text2;
	public Text text3;

	private string[] lessonNameTexts = { "Earth", "The Sun", "Orbit", "The Moon", 
		"Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };

	// Info panel texts
	private string[] texts1 = { "Earth is our home planet. It is the third planet from the Sun, and the only planet in the Solar System that is inhabited by living things.", 
								"The Sun is located in the center of the Solar System. It is the largest and hottest object in the Solar System, and the brightest object in the sky.", 
								"Every planet in the Solar System revolves around the Sun. This path that the planets take through space is called an orbit.", 
								"The Moon is the natural satellite of our planet, Earth. It is Earth's only natural satellite and it revolves around Earth about every 27 days.", 
								"Mercury is the closest planet in the Solar System to the Sun, and also the smallest. Unlike Earth, it does not have any natural satellites.", 
								"Venus is the second planet from the Sun. It is the third brightest object (after the Sun and the Moon) and the brightest planet in the sky.", 
								"Mars is the fourth planet from the Sun and the second-smallest planet in the Solar System. It is almost twice as small as Earth.", 
								"Jupiter is the fifth planet from the Sun and the largest planet in the Solar System. It is also the fourth brightest object in the sky (after the Sun, Moon and Venus).", 
								"Saturn is the sixth planet from the Sun and the second largest planet in the Solar System. Like Jupiter, it is also a gas giant.", 
								"Uranus is the seventh planet from the Sun. Along with Neptune, it is classified as an ice giant due to it being composed of heavier substances, known as \"ices\".", 
								"Neptune is the eighth and the farthest planet from the Sun. It is more than 30 times farther from the Sun than Earth is." };

	private string[] texts2 = { "Earth is estimated to be around 4.5 billion years old, which is around the same time as the formation of the Solar System.",
								"It is the most important source of energy for life on Earth, converting 4 million tons of matter into energy every second.",
								"The planets' orbits are caused by the gravity of the Sun, whose massive force helps them to stay in orbit.",
								"The Moon is the second brightest object in the sky, after the Sun. It appears bright in the night sky because it reflects the Sun's light.",
								"During the day, its surface temperature can reach up to 800 degrees Fahrenheit / 430 degrees Celsius, and can drop to as low as -290 degrees Fahrenheit / -180 degrees Celsius at night.",
								"Venus has gases and clouds of sulfuric acid that traps the heat in its atmosphere, making it the hottest planet in the Solar System. Yep, even hotter than Mercury.",
								"Mars is the outermost terrestrial planet, and its distance from the Sun is twice as far as that of the Earth. It has two natural satellites: Phobos and Deimos.",
								"Jupiter is not a terrestrial planet but a gas giant, which means it is composed mainly of hydrogen and helium, with a molten rocky core inside.",
								"Saturn is the farthest planet from Earth that is visible in the night sky. It has 82 confirmed moons: the most moons that a planet has in the Solar System.",
								"Uranus rotates in the opposite direction than most planets. It is also the only planet known to rotate on its side.",
								"Like Uranus, Neptune's color is also caused by the presence of methane. Though Uranus has the coldest temperature ever recorded, Neptune has the coldest overall average temperature." };

	private string[] texts3 = { "Earth is a terrestrial planet, meaning that the planet is mostly made of silicate rocks or metals. It is the largest terrestrial planet in the Solar System.",
								"The Sun is also the heaviest object in the Solar System, accounting for 99.86% of the total mass of the entire Solar System!",
								"The planets travel around the Sun at different speeds; the closer the planet is to the Sun, the faster it needs to travel to maintain its orbit.",
								"The Moon rotates in the same rate as it takes to orbit the Earth, causing one side of the Moon to always be facing the Earth.",
								"Like Earth, Mercury is also a terrestrial planet, along with Venus and Mars. It orbits around the Sun once every 88 days, making its path the shortest of all planets.",
								"Venus takes 243 days to complete one rotation. This is the slowest rotation of any planet, making it the most spherical planet in the Solar System.",
								"Mars is commonly known as the Red Planet. Its reddish appearance is caused by the presence of rusty iron on its surface. The name of the month March is derived from Mars.",
								"Jupiter is covered in swirling cloud stripes and has enormous storms like the Great Red Spot. A day on Jupiter goes by very fast: it only lasts for about 10 hours.",
								"The rings of Saturn are very bright and are composed of many chunks of ice and rock that circle Saturn at their own speeds. These objects range from a centimeter to a few kilometers!",
								"Uranus is made of water, methane, and ammonia fluids above a small rocky center. The presence of methane is what makes Uranus appear blue.",
								"Neptune is a dark and windy planet, having the most powerful winds in the Solar System. Its wind speeds may reach up to 2,160 kilometers / 1,324 miles per hour!" };

	// Detail window
	public GameObject detailWindow;
	public TextMeshPro planetNameText;
	public TextMeshPro detailText;
	//										   Earth	Sun				 Moon	  Mercury	Venus	   Mars		Jupiter	   Saturn	  Uranus	Neptune
	private string[] massValues =			 { "5.97",  "1,988,500", "", "0.073", "0.330",  "4.87",	   "0.642", "1,898",   "568",	  "86.8",	"102"	 };
	private string[] diameterValues =		 { "4,879", "1,392,700", "", "3,475", "4,879",  "12,104",  "6,792", "142,984", "120,536", "51,118", "49,528" };
	private string[] densityValues =		 { "5,514", "1,408",	 "", "3,340", "5,427",  "5,243",   "3,933", "1,326",   "6,87",	  "1,271",	"1,638"	 };
	private string[] gravityValues =		 { "9.8",	"274",		 "", "1.6",	  "3.7",    "8.9",	   "3.7",	"23.1",	   "9.0",	  "8.7",	"11.0"   };
	private string[] rotationPeriodValues =  { "23.9",  "609.1",	 "", "655.7", "1407.6", "-5832.5", "24.6",	"9.9",	   "10.7",	  "-17.2",	"16.1"	 };
	private string[] lengthOfDayValues =	 { "24.0",  "N/A",		 "", "708.7", "4222.6", "2802.0",  "24.7",	"9.9",	   "10.7",	  "17.2",	"16.1"	 };
	private string[] distanceFromSunValues = { "149.6", "N/A",		 "", "N/A",	  "57.9",	"108.2",   "227.9", "778.6",   "1433.5",  "2872.5", "4495.1" };
	private string[] orbitalPeriodValues =	 { "365.2", "N/A",		 "", "27.3",  "88.0",	"224.7",   "687.0", "4,331",   "10,747",  "30,589", "59,800" };
	private string[] orbitalVelocityValues = { "29.8",  "N/A",		 "", "1.0",   "47.4",	"35.0",    "24.1",	"13.1",    "9.7",	  "6.8",	"5.4"	 };
	private string[] numberOfMoonsValues =	 { "1",		"0",		 "", "N/A",   "0",		"0",	   "2",		"79",	   "82",	  "27",		"14"	 };

	// Audio clips
	public AudioSource[] clips1;
	public AudioSource[] clips2;
	public AudioSource[] clips3;

	private AudioSource lessonAudio;

	void Start()
	{
		topicNumber = PlayerPrefs.GetInt("TOPIC NUMBER");

		// Set virtual object.
		virtualObject = virtualObjects[topicNumber];
		virtualObject.SetActive(true);

		/*
		// Set the detail window.
		if (topicNumber == 2)
		{
			detailWindow.SetActive(false);
		}
		else
		{
			detailWindow.SetActive(true);
		}
		*/

		detailWindow.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Set the virtual objects and the texts.
		lessonNameText.text = lessonNameTexts[topicNumber];
		text1.text = texts1[topicNumber];
		text2.text = texts2[topicNumber];
		text3.text = texts3[topicNumber];

		// Set detail window text.
		planetNameText.text = virtualObjects[topicNumber].name;
		detailText.text = "Mass (10^24kg): " + massValues[topicNumber]
						+ "\nDiameter (km): " + diameterValues[topicNumber]
						+ "\nDensity (kg/m^3): " + densityValues[topicNumber]
						+ "\nGravity (m/s^2): " + gravityValues[topicNumber]
						+ "\nRotation period (hours): " + rotationPeriodValues[topicNumber]
						+ "\nLength of day (hours): " + lengthOfDayValues[topicNumber]
						+ "\nDistance from Sun (10^6km): " + distanceFromSunValues[topicNumber]
						+ "\nOrbital period (days): " + orbitalPeriodValues[topicNumber]
						+ "\nOrbital velocity (km/s): " + orbitalVelocityValues[topicNumber]
						+ "\nNumber of moons: " + numberOfMoonsValues[topicNumber];

		if (text1.gameObject.activeSelf)
        {
			lessonAudio = clips1[topicNumber];
        }
		else if (text2.gameObject.activeSelf)
        {
			lessonAudio = clips2[topicNumber];
		}
		else if (text3.gameObject.activeSelf)
        {
			lessonAudio = clips3[topicNumber];
		}
	}

	public void OnClickSoundButton()
	{
		if (lessonAudio.isPlaying)
		{
			lessonAudio.Stop();
		}
		else
		{
			lessonAudio.Play();
		}
	}

	public void OnClickDetailButton()
	{
		if (detailWindow.activeSelf)
		{
			detailWindow.SetActive(false);
		}
		else
		{
			detailWindow.SetActive(true);
		}
	}

	

	public void OnClickInfoButton() // Open the info panel
    {
		if (infoPanel.activeSelf)
        {
			infoPanel.SetActive(false);
        }
		else
        {
			infoPanel.SetActive(true);
		}
    }

	public void OnClickPreviousButton() // Switch between texts
	{
		if (text2.IsActive())
		{
			text2.gameObject.SetActive(false);
			previousButton.gameObject.SetActive(false);
			text1.gameObject.SetActive(true);
		}
		else if (text3.IsActive())
		{
			text3.gameObject.SetActive(false);
			text2.gameObject.SetActive(true);
			nextButton.gameObject.SetActive(true);
			finishButton.gameObject.SetActive(false);
		}

		lessonAudio.Stop();
	}

	public void OnclickNextButton() // Swtich between texts
    {
		if (text1.IsActive())
        {
			text1.gameObject.SetActive(false);
			previousButton.gameObject.SetActive(true);
			text2.gameObject.SetActive(true);
        }
		else if (text2.IsActive())
        {
			text2.gameObject.SetActive(false);
			text3.gameObject.SetActive(true);
			nextButton.gameObject.SetActive(false);
			finishButton.gameObject.SetActive(true);
		}

		lessonAudio.Stop();
	}

	public void OnClickFinishButton()
	{
		virtualObject.SetActive(false);
		infoPanel.SetActive(false);
		successPanel.SetActive(true);
		lessonNameText.gameObject.SetActive(false);
		detailWindow.SetActive(false);

		backButton.interactable = false;
		infoButton.interactable = false;
		rotateButton.interactable = false;
		stopRotateButton.interactable = false;
		soundButton.interactable = false;

		if (PlayerPrefs.GetString("ISLOGGEDIN") == "True")
		{
			// If next topic is a quiz, set quiz number to prepare for quiz
			if (topicNumber == 3)
			{
				PlayerPrefs.SetInt("QUIZ NUMBER", 0);
			}
			else if (topicNumber == 6)
			{
				PlayerPrefs.SetInt("QUIZ NUMBER", 1);
			}
			else if (topicNumber == 10)
			{
				PlayerPrefs.SetInt("QUIZ NUMBER", 2);
			}
			else
			{
				// If next topic is not a quiz, increase topicNumber to prepare for next topic.
				topicNumber++;
			}

			// If player hasn't already unlocked next quiz, unlock next quiz.
			if (PlayerPrefs.GetInt("QUIZ NUMBER") + 1 > PlayerPrefs.GetInt("UNLOCKED QUIZZES"))
			{
				PlayerPrefs.SetInt("UNLOCKED QUIZZES", PlayerPrefs.GetInt("QUIZ NUMBER") + 1);
			}

			// Each time the player finishes a lesson, increase the number of lessons completed by 1.
			PlayerPrefs.SetInt("LESSONS COMPLETED", PlayerPrefs.GetInt("LESSONS COMPLETED") + 1);

			// Each time the player finishes a lesson, update player data.
			PlayfabController.controller.SetPlayerData(PlayerPrefs.GetInt("UNLOCKED TOPICS"),
			PlayerPrefs.GetInt("LESSONS COMPLETED"), PlayerPrefs.GetInt("UNLOCKED QUIZZES"));
		}
		else
		{
			// If player is not logged in and hasn't completed the first four lessons, continue to next topic.
			topicNumber++;
		}

		// If user finishes topic, unlock next topic.
		if (topicNumber > PlayerPrefs.GetInt("UNLOCKED TOPICS"))
		{
			PlayerPrefs.SetInt("UNLOCKED TOPICS", topicNumber);
		}
	}

	public void OnClickStopRotateButton() // Toogle object movement
	{
		stopRotateButton.gameObject.SetActive(false);
		rotateButton.gameObject.SetActive(true);

		if (virtualObject.tag == "Solar System")
		{
			foreach (Transform child in virtualObject.transform)
			{
				if (child.tag == "AR Object")
				{
					if (child.gameObject.GetComponent<Orbit>() != null)
					{
						child.gameObject.GetComponent<Orbit>().enabled = false;
					}
					if (child.gameObject.GetComponent<Rotate>() != null)
					{
						child.gameObject.GetComponent<Rotate>().enabled = false;
					}
				}
			}
		}
		else
		{
			if (virtualObject.GetComponent<Orbit>() != null)
			{
				virtualObject.GetComponent<Orbit>().enabled = false;
			}
			if (virtualObject.GetComponent<Rotate>() != null)
			{
				virtualObject.GetComponent<Rotate>().enabled = false;
			}
		}
	}

	public void OnClickRotateButton() // Toggle object movement
	{
		rotateButton.gameObject.SetActive(false);
		stopRotateButton.gameObject.SetActive(true);

		if (virtualObject.tag == "Solar System")
		{
			foreach (Transform child in virtualObject.transform)
			{
				if (child.tag == "AR Object")
				{
					if (child.gameObject.GetComponent<Orbit>() != null)
					{
						child.gameObject.GetComponent<Orbit>().enabled = true;
					}
					if (child.gameObject.GetComponent<Rotate>() != null)
					{
						child.gameObject.GetComponent<Rotate>().enabled = true;
					}
				}
			}
		}
		else
		{
			if (virtualObject.GetComponent<Orbit>() != null)
			{
				virtualObject.GetComponent<Orbit>().enabled = true;
			}
			if (virtualObject.GetComponent<Rotate>() != null)
			{
				virtualObject.GetComponent<Rotate>().enabled = true;
			}
		}
	}

	public void NextLesson()
    {
		// If player has reached the end of pathway, go back to pathway scene.
		if (topicNumber == virtualObjects.Length)
        {
			SceneManager.LoadScene("Pathway Scene");
        }
		else if (topicNumber == 4 && PlayerPrefs.GetString("ISLOGGEDIN") == "False")
        {
			// If player wishes to continue learning but is not logged in, they will have to log in.
			successPanel.SetActive(false);
			loginMessagePanel.SetActive(true);
			Debug.Log("Log in to continue");
			// SceneManager.LoadScene("Pathway Scene");
		}		
		else
        {
			// Check if player has reached the quiz section.
			// If yes, continue to quiz. If not, continue to next topic
			PlayerPrefs.SetInt("TOPIC NUMBER", topicNumber);
			if ((topicNumber == 3 && PlayerPrefs.GetInt("QUIZ NUMBER") == 0) 
				|| (topicNumber == 6 && PlayerPrefs.GetInt("QUIZ NUMBER") == 1) 
				|| (topicNumber == 10 && PlayerPrefs.GetInt("QUIZ NUMBER") == 2))
            {
				SceneManager.LoadScene("Quiz Scene");
            }
			else
            {
				SceneManager.LoadScene("Learn Scene");
			}
		}
    }
}
