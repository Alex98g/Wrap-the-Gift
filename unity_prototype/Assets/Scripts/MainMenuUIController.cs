using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class MainMenuUIController : MonoBehaviour
{
    public AudioSource backgroundSound;
    public AudioSource Sound_ClickBtn;
    public GameObject MainMenu;
    public GameObject MainMenuUI;
    public GameObject Settings;
    // Start is called before the first frame update
    void Start()
    {
        backgroundSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        MainMenu.active = true;
        MainMenuUI.active = true;
        Settings.active = false;

    }
    public void onSettingstBtn()
    {
        Sound_ClickBtn.Play();
        StartCoroutine(waiter_Settings());

    }
    IEnumerator waiter_Settings()
    {
        yield return new WaitForSeconds(2);
        MainMenu.active = false;
        MainMenuUI.active = false;
        Settings.active = true;

    }
}
