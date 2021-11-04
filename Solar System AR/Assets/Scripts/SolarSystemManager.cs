using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SolarSystemManager : MonoBehaviour
{
    public Button finishButton;
    public Button nextButton;
    public Button previousButton;
    public Button rotateButton;
    public Button stopRotateButton;

	public Text text1;
	public Text text2;
	public Text text3;

	public GameObject infoPanel;
	public GameObject label;
	public GameObject solarSystem;

	public GameObject sun, mercury, venus, earth, mars, jupiter, saturn, uranus, neptune;

	// Detail window
	public GameObject detailWindow;
	public TextMeshPro planetNameText;
	public TextMeshPro detailText;
	public TextMeshPro previewText;
	private int planetNum;

	//										   Sun			Mercury	  Venus		 Earth	  Mars	   Jupiter	  Saturn	 Uranus	   Neptune
	private string[] massValues =			 { "1,988,500", "0.330",  "4.87",    "5.97",  "0.642", "1,898",   "568",	 "86.8",   "102"	};
	private string[] diameterValues =		 { "1,392,700", "4,879",  "12,104",  "4,879", "6,792", "142,984", "120,536", "51,118", "49,528" };
	private string[] densityValues =		 { "1,408",		"5,427",  "5,243",	 "5,514", "3,933", "1,326",   "6,87",	 "1,271",  "1,638"	};
	private string[] gravityValues =		 { "274",		"3.7",	  "8.9",	 "9.8",   "3.7",   "23.1",    "9.0",	 "8.7",	   "11.0"	};
	private string[] rotationPeriodValues =  { "609.1",		"1407.6", "-5832.5", "23.9",  "24.6",  "9.9",	  "10.7",	 "-17.2",  "16.1"	};
	private string[] lengthOfDayValues =	 { "N/A",		"4222.6", "2802.0",  "24.0",  "24.7",  "9.9",	  "10.7",	 "17.2",   "16.1"	};
	private string[] distanceFromSunValues = { "N/A",		"57.9",	  "108.2",	 "149.6", "227.9", "778.6",   "1433.5",  "2872.5", "4495.1" };
	private string[] orbitalPeriodValues =	 { "N/A",		"88.0",	  "224.7",	 "365.2", "687.0", "4,331",   "10,747",  "30,589", "59,800" };
	private string[] orbitalVelocityValues = { "N/A",		"47.4",	  "35.0",	 "29.8",  "24.1",  "13.1",    "9.7",	 "6.8",	   "5.4"	};
	private string[] numberOfMoonsValues =	 { "0",			"0",	  "0",		 "1",	  "2",	   "79",	  "82",		 "27",	   "14"		};


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// If user taps on a planet, the detail window will display the info of that planet.
		if (Input.GetMouseButtonDown(0))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == sun.name)
                {
					planetNameText.text = sun.name;
					planetNum = 0;
				}
                else if (hit.transform.name == mercury.name)
                {
					planetNameText.text = mercury.name;
					planetNum = 1;
				}
				else if (hit.transform.name == venus.name)
				{
					planetNameText.text = venus.name;
					planetNum = 2;
				}
				else if (hit.transform.name == earth.name)
				{
					planetNameText.text = earth.name;
					planetNum = 3;
				}
				else if (hit.transform.name == mars.name)
				{
					planetNameText.text = mars.name;
					planetNum = 4;
				}
				else if (hit.transform.name == jupiter.name)
				{
					planetNameText.text = jupiter.name;
					planetNum = 5;
				}
				else if (hit.transform.name == saturn.name)
				{
					planetNameText.text = saturn.name;
					planetNum = 6;
				}
				else if (hit.transform.name == uranus.name)
				{
					planetNameText.text = uranus.name;
					planetNum = 7;
				}
				else if (hit.transform.name == neptune.name)
				{
					planetNameText.text = neptune.name;
					planetNum = 8;
				}

				previewText.gameObject.SetActive(false);

				detailText.text = "Mass (10^24kg): " + massValues[planetNum]
						+ "\nDiameter (km): " + diameterValues[planetNum]
						+ "\nDensity (kg/m^3): " + densityValues[planetNum]
						+ "\nGravity (m/s^2): " + gravityValues[planetNum]
						+ "\nRotation period (hours): " + rotationPeriodValues[planetNum]
						+ "\nLength of day (hours): " + lengthOfDayValues[planetNum]
						+ "\nDistance from Sun (10^6km): " + distanceFromSunValues[planetNum]
						+ "\nOrbital period (days): " + orbitalPeriodValues[planetNum]
						+ "\nOrbital velocity (km/s): " + orbitalVelocityValues[planetNum]
						+ "\nNumber of moons: " + numberOfMoonsValues[planetNum];
			}
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

	public void OnclickInfoButton()
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
			// finishButton.gameObject.SetActive(true);
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
			// finishButton.gameObject.SetActive(false);
		}
	}

	public void OnClickStopRotateButton() // Toogle object movement
	{
		stopRotateButton.gameObject.SetActive(false);
		rotateButton.gameObject.SetActive(true);

		foreach (Transform child in solarSystem.transform)
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

		foreach (Transform child in label.transform)
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

	public void OnClickRotateButton() // Toggle object movement
	{
		rotateButton.gameObject.SetActive(false);
		stopRotateButton.gameObject.SetActive(true);

		foreach (Transform child in solarSystem.transform)
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

		foreach (Transform child in label.transform)
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
