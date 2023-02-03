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

        //public GameObject EnvironmentHolder;

        private Text ClientText; //text for count client
        private Text CoinText; //text for count money (coin)
        private Text TimeText; //text for count time

        private List<GameObject> presentsButtons = new List<GameObject>();
        private List<GameObject> ribbonsButtons = new List<GameObject>();
        
        public GameObject BtnRibbon;
        public GameObject BtnBox;

        GameSceneController GameSceneController;

        public AudioSource Sound_ClickBtn;

        public Text Label_count_level;

        public ScrollRect RibbonScrollView;
        public ScrollRect PresentScrollView;

        

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
            initPresentScrollBar();
            initRibbonScrollBar();
        }

        // Update is called once per frame
        void Update()
        {
            if (briefTimer > 0)
            {
                briefTimer -= Time.deltaTime;
                return;
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
            //var LevelImage = BriefMenuUI.transform.Find("LevelImage").GetComponent<Image>();
            //var pathToimage = $"LevelIcons/2x/Level_{PlayerData.levelToLoad + 1}@2x";
            //var sprite = Resources.Load<Sprite>(pathToimage);
            //LevelImage.sprite = sprite;
        }

        public void initRibbonScrollBar()
        {

            var content = GameUI.transform.Find("RibbonScrollView").GetComponent<ScrollRect>().content;
            
            //var content = EnvironmentHolder.transform.Find("RibbonScrollView").GetComponent<ScrollRect>().content;
            foreach (var color in PlayerData.AvailableRibbonColors)
            {
                GameObject btn = Instantiate(BtnRibbon);
                var ribbon = btn.transform.Find("ribbonColor");
                ribbonsButtons.Add(btn);
                btn.transform.SetParent(content, false);
                ribbon.GetComponent<UnityEngine.UI.Image>().color = color.ToColor();
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangeRibbonColor(color));
            }
            /*Canvas.ForceUpdateCanvases();
            if (content.GetComponent<RectTransform>().sizeDelta.y > 0)
            {
                content.localPosition = new Vector3(0f, content.GetComponent<RectTransform>().sizeDelta.y / 5f, 0f);
            }*/
        }

        private void initPresentScrollBar()
        {
         
            var content = GameUI.transform.Find("PresentScrollView").GetComponent<ScrollRect>().content;
            foreach (var color in PlayerData.AvailablePresentColor)
            {
                GameObject btn = Instantiate(BtnBox);
                var box = btn.transform.Find("boxColor");
                presentsButtons.Add(btn);
                btn.transform.SetParent(content, false);
                box.GetComponent<UnityEngine.UI.Image>().color = color.ToColor();
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangePresentColor(color));
            }
            /*Canvas.ForceUpdateCanvases();
            if (content.GetComponent<RectTransform>().sizeDelta.y > 0)
            {
                content.localPosition = new Vector3(0f, content.GetComponent<RectTransform>().sizeDelta.y / 5f, 0f);
            }*/
        }


        


        //--------------------------------------------//
        public void onPauseClickCallback()
        {
            Sound_ClickBtn.Play();
            GameSceneController.TogglePause();
            OnPause();
        }
        //--------------------------------------------//
        public void onResumeClickCallback()
        {
            Sound_ClickBtn.Play();
            GameSceneController.TogglePause();
            OnResume();
        }
        //--------------------------------------------//
        public void onRestartClickCallback()
        {
            Sound_ClickBtn.Play();
            throw new NotImplementedException();
        }
        //--------------------------------------------//
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
        //--------------------------------------------//
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

        //--------------------------------------------//
        public void onSettings()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(false);
            Settings.SetActive(true);
        }
        //--------------------------------------------//

        public void onBackMenuPause()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(true);
            Settings.SetActive(false);
        }
        //--------------------------------------------//
        public void Clearribbons()
        {
            for (int i = 0; i < RibbonScrollView.content.childCount; i++)
            {
                Destroy(RibbonScrollView.content.GetChild(i).gameObject);
            }
        }
        public void Clearpapers()
        {
            for (int i = 0; i < PresentScrollView.content.childCount; i++)
            {
                Destroy(PresentScrollView.content.GetChild(i).gameObject);
            }
        }
        //--------------------------------------------//
        public void onBackMenuPausefromStore()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(true);
            StoreUI.SetActive(false);
            Clearribbons();
            Clearpapers();
            initRibbonScrollBar();
            initPresentScrollBar();
        }

        //--------------------------------------------//
        public void onStore()
        {
            Sound_ClickBtn.Play();
            PauseUI.SetActive(false);
            StoreUI.SetActive(true);
            
        }
        //--------------------------------------------//
        private void DeactivateAllUI()
        {
            IsBrief = false;
            var menuUIs = new List<GameObject> { PauseMenuUI, GameUI, WinMenuUI, LoseMenuUI, BriefMenuUI, StoreUI };
            foreach (var menuUI in menuUIs)
            {
                menuUI.SetActive(false);
            }
        }
        //--------------------------------------------//
        public void OnWin()
        {
            WinMenuUI.SetActive(true);
        }
        //--------------------------------------------//
        public void OnLose()
        {
            
            LoseMenuUI.SetActive(true);
        }
        //--------------------------------------------//
        public void onStart() //for button Start
        {
            DeactivateAllUI();
            BriefMenuUI.SetActive(true);
            IsBrief = true;
            
        }
        //--------------------------------------------//
        public void OnResume() //for button Menu
        {
            DeactivateAllUI();
            GameUI.SetActive(true);
        }
        //--------------------------------------------//
        public void OnPause()
        {
            
            PauseMenuUI.SetActive(true);
        }
        //--------------------------------------------//
        public void ChangePresentColor(PresentColor color)
        {
            GameObject.Find("GiftTable").GetComponent<MaterialManager>().WrapColor = color.ToColor();
            GameSceneController.CurPresentColor = color;
        }
        //--------------------------------------------//
        public void ChangeRibbonColor(RibbonColor color)
        {
            GameObject.Find("GiftTable").GetComponent<MaterialManager>().RibbonColor = color.ToColor();
            GameSceneController.CurRibbonColor = color;
        }
        //--------------------------------------------//
        public void UpdateClientCounter(int current, int common)
        {
            ClientText.text = $"{current}/{common}";
        }
        //--------------------------------------------//
        public void UpdateCoinCounter(int current)
        {
            CoinText.text = $"{current}";
        }
        //--------------------------------------------//
        public void UpdateLevelTimer(float timeLeft)
        {
            TimeText.text = $"{timeLeft:0.00}";
        }
    }
}
