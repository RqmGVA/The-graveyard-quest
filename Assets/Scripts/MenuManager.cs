using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject newPowerPanel;
    public GameObject introductionPanel;
    public GameObject improveSkillPanel;
    public GameObject room2Panel;

    [SerializeField] private GameObject BreakPanel;
    [SerializeField] private GameObject lifeBarCanvas;

    public bool isPanelActive = false;
    private bool isBreakMenuActive = false;

    void Update()
    {
        //Panels about signs
        if (isPanelActive == true)
        {
            lifeBarCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        //Break Panel
        else if (isBreakMenuActive == true)
        {
            Time.timeScale = 0;
            lifeBarCanvas.SetActive(false);
            BreakPanel.SetActive(true);

        }
        else
        {
            Time.timeScale = 1;
            lifeBarCanvas.SetActive(true);
            BreakPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeButton();
        }
    }
    public void IGotIt()
    {
        introductionPanel.SetActive(false);
        newPowerPanel.SetActive(false);
        improveSkillPanel.SetActive(false);
        room2Panel.SetActive(false);
        isPanelActive = false;
    }

    public void ResumeButton()
    {
        isBreakMenuActive = !isBreakMenuActive;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
