using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject hostMenu;
	public GameObject playerPanels;
	public GameObject mainButtons;
	public GameObject logo;

	string mainMenuName = "";

	static MainMenu instance = null;
	public static MainMenu Instance => instance;
	// Start is called before the first frame update
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			//DontDestroyOnLoad(hostMenu);

			//DontDestroyOnLoad(playerPanels);

			mainMenuName = SceneManager.GetActiveScene().name;
		}
		else
		{
			Destroy(gameObject);
		}

	}

	public void GoToScene(string sceneName)
	{
		if (sceneName == mainMenuName)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			mainButtons.SetActive(true);


			logo.SetActive(true);
		}

		else
			Cursor.visible = false;

		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void OpenHostWindow()
    {
		hostMenu.SetActive(true);
		mainButtons.SetActive(false);
    }

	public void QuitApplication()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (SceneManager.GetActiveScene().name == mainMenuName)
				Application.Quit();
			else
				GoToScene(mainMenuName);
		}


		if (SceneManager.GetActiveScene().name != mainMenuName)
			gameObject.SetActive(false);
	}
}
