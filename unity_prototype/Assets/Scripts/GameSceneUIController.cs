using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Game
{
    public class GameSceneUIController : MonoBehaviour
    {
        private float briefTimer;
        public bool IsBrief = true;
        private GameObject PauseMenuUI; //scene Pause
        public GameObject PauseUI;
        public GameObject Settings;

        private GameObject GameUI;
        private GameObject WinMenuUI; //scene Win
        private GameObject LoseMenuUI; //scene Lose
        private GameObject BriefMenuUI; //scene Level
        public GameObject StoreUI;

        private Text ClientText; //text for count client
        private Text CoinText; //text for count money (coin)
        private Text TimeText; //text for count time

        public GameObject BtnRibbon;
        public GameObject BtnBox;

        GameSceneController GameSceneController;

        public AudioSource Sound_ClickBtn;

        public Text Label_count_level;

        public ScrollRect RibbonScrollView;
        public ScrollRect PresentScrollView;

        List<IList> Gift = new List<IList>()
        {
            PlayerData.AvailablePresentColor,
            PlayerData.AvailableRibbonColors
        };
        
        
        void Start()
        {
            
            GameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();

            PauseMenuUI = GameObject.Find("PauseMenu");
            GameUI = GameObject.Find("GameUI");
            WinMenuUI = GameObject.Find("WinMenuUI");
            LoseMenuUI = GameObject.Find("LoseMenuUI");
            BriefMenuUI = GameObject.Find("BriefMenuUI");

            ClientText = GameObject.Find("ClientLbl").GetComponent<Text>();
            CoinText = GameObject.Find("CoinLbl").GetComponent<Text>();
            TimeText = GameObject.Find("TimeLbl").GetComponent<Text>();
            
            PlayerData.CoinCounter = 0;
            briefTimer = GameSceneController.briefTime;
            onStart();
            GameSceneController.LoadGame();
            InitBrief();
            updatelistGift();
            initScrollBars(Gift[0], BtnBox, PresentScrollView.content, "boxColor");
            initScrollBars(Gift[1], BtnRibbon, RibbonScrollView.content, "ribbonColor");
            
        }

        public void initScrollBars(IList gift, GameObject btn, RectTransform content, string obj)
        {
            foreach (var color in gift)
            {
                GameObject button = Instantiate(btn);
                button.name = color.ToString();
                var box = button.transform.Find(obj);
                if (content.Find(button.name) == null)
                {
                    button.transform.SetParent(content, false);
                }

                if (color is PresentColor)
                {
                    PresentColor colorchangetype = (PresentColor)color;
                    box.GetComponent<UnityEngine.UI.Image>().color = colorchangetype.ToColor();
                    button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangePresentColor(colorchangetype));
                }
                else
                {
                    RibbonColor colorchangetype = (RibbonColor)color;
                    box.GetComponent<UnityEngine.UI.Image>().color = colorchangetype.ToColor();
                    button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangeRibbonColor(colorchangetype));
                }
            }
        }

        void Update()
        {
            if (briefTimer > 0)
            {
                briefTimer -= Time.deltaTime;
            }
            else if (IsBrief)
            {
                OnResume();
            }
        }

        private void InitBrief()
        {
            BriefMenuUI.SetActive(true);
            IsBrief = true;
            GameSceneController.State = GameSceneController.GameState.Brief;
            Label_count_level.text = $"{PlayerData.levelToLoad + 1}";
        }

        public void onPauseClickCallback()
        {
            Sound_ClickBtn.Play();
            GameSceneController.TogglePause();
            OnPause();
        }

        public void onResumeClickCallback()
        {
            Sound_ClickBtn.Play();
            GameSceneController.TogglePause();
            OnResume();
        }

        public void onRestartClickCallback()
        {
            Sound_ClickBtn.Play();
            throw new NotImplementedException();
        }
        
        public void onBackToMenuClickCallback()
        {
            Sound_ClickBtn.Play();
            Time.timeScale = 1f;
            StartCoroutine(waiter_BackToMenu());
        }
        IEnumerator waiter_BackToMenu()
        {
            yield return new WaitForSeconds(2);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
        }

        public void onPlayNextLevelClickCallback()
        {
            Sound_ClickBtn.Play();
            Time.timeScale = 1f;
            StartCoroutine(waiter());
            
        }
        IEnumerator waiter()
        {
            yield return new WaitForSeconds(2);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }

        public void onSettings()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(false);
            Settings.SetActive(true);
        }

        public void onBackMenuPause()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(true);
            Settings.SetActive(false);
        }
        
        public void onBackMenuPausefromStore()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(true);
            StoreUI.SetActive(false);
            updatelistGift();
            initScrollBars(Gift[0], BtnBox, PresentScrollView.content, "boxColor");
            initScrollBars(Gift[1], BtnRibbon, RibbonScrollView.content, "ribbonColor");
        }

        public void updatelistGift()
        {
            Gift = new List<IList>()
            {
                PlayerData.AvailablePresentColor,
                PlayerData.AvailableRibbonColors
            };
        }

        public void onStore()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(false);
            StoreUI.SetActive(true);
        }

        private void DeactivateAllUI()
        {
            IsBrief = false;
            var menuUIs = new List<GameObject> { PauseMenuUI, GameUI, WinMenuUI, LoseMenuUI, BriefMenuUI, StoreUI };
            foreach (var menuUI in menuUIs)
            {
                menuUI.SetActive(false);
            }
        }
       
        public void OnWin()
        {
            WinMenuUI.SetActive(true);
        }
        
        public void OnLose()
        {
            
            LoseMenuUI.SetActive(true);
        }
        
        public void onStart() //for button Start
        {
            DeactivateAllUI();
            BriefMenuUI.SetActive(true);
            IsBrief = true;
        }
       
        public void OnResume() //for button Menu
        {
            DeactivateAllUI();
            GameUI.SetActive(true);
        }
        
        public void OnPause()
        {
            
            PauseMenuUI.SetActive(true);
        }
        
        public void ChangePresentColor(PresentColor color)
        {
            
            GameObject.Find("GiftTable").GetComponent<MaterialManager>().PresentColor = color.ToColor();
            GameSceneController.CurPresentColor = color;
        }
        
        public void ChangeRibbonColor(RibbonColor color)
        {
            GameObject.Find("GiftTable").GetComponent<MaterialManager>().RibbonColor = color.ToColor();
            GameSceneController.CurRibbonColor = color;
        }
        
        public void UpdateClientCounter(int current, int common)
        {
            ClientText.text = $"{current}/{common}";
        }
        
        public void UpdateCoinCounter(int current)
        {
            CoinText.text = $"{current}";
        }
        
        public void UpdateLevelTimer(float timeLeft)
        {
            TimeText.text = $"{timeLeft:0.00}";
        }
    }

}
