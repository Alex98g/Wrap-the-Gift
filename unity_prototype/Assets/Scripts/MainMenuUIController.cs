using System.Collections;
using UnityEngine;
using Game;
public class MainMenuUIController : MonoBehaviour
{
    public AudioSource backgroundSound;
    public AudioSource Sound_ClickBtn;
    public GameObject MainMenu;
    public GameObject MainMenuUI;
    public GameObject Settings;
    
    void Start()
    {
        backgroundSound.Play();
    }

    public void onPlayBtnClickCallback()
    {
        Sound_ClickBtn.Play();
        StartCoroutine(waiter_Play());
    }
    IEnumerator waiter_Play()
    {
        yield return new WaitForSeconds(2);
        GameSceneController.LoadGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void onExitBtn()
    {
        Sound_ClickBtn.Play();
        StartCoroutine(waiter_Quit());
        
    }
    IEnumerator waiter_Quit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
    public void onBacktBtnSettings()
    {
        Sound_ClickBtn.Play();
        StartCoroutine(waiter_BackSettings());

    }
    IEnumerator waiter_BackSettings()
    {
        yield return new WaitForSeconds(2);
        MainMenu.SetActive(true);
        MainMenuUI.SetActive(true);
        Settings.SetActive(false);

    }
    public void onSettingstBtn()
    {
        Sound_ClickBtn.Play();
        StartCoroutine(waiter_Settings());

    }
    IEnumerator waiter_Settings()
    {
        yield return new WaitForSeconds(2);
        MainMenu.SetActive(false);
        MainMenuUI.SetActive(false);
        Settings.SetActive(true);

    }
}
