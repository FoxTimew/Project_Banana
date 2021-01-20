using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject startFirst, optionFirst, closeOption;
    public GameObject option, mainMenu;
    public float dureeAnimation;
    public DisableCanvas SceneTransition;
    public Animator anim;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startFirst);
    }

    private void Update()
    {
       
    }
    public void PlayGame()
    {
        StartCoroutine(ChangeScene());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OptionMenu()
    {
        mainMenu.SetActive(false);
        option.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionFirst);
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        option.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeOption);
    }

    IEnumerator ChangeScene()
    {
        anim.Play("UI_StartMenu");
        yield return new WaitForSeconds(dureeAnimation);
        SceneTransition.TPTransition();
        yield return new WaitForSeconds(dureeAnimation);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
