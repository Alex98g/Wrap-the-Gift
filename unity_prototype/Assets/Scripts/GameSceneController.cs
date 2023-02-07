using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Game
{
    public class GameSceneController : MonoBehaviour
    {
        public float levelTimer;
        public enum GameState
        {
            Idle,
            Brief,
            ClientComing,
            ClientLeaving,
            PreWon,
            PreLose,
            Won,
            Lose,
        }

        public GameState State = GameState.Brief;

        public bool isGameStarted = false;
        public bool IsGamePaused = false;
        public float briefTime;

        
        public List<GameObject> ClientsPrefab;
      
        public ClientController CurrentClient;
        private GameSceneUIController UIController;
        private GameObject EnvironmentHolder;
        private LevelInfo data;
        private int ClientIdx = -1;
        private GameObject ClientsHolder;
        public PresentColor CurPresentColor;
        public RibbonColor CurRibbonColor;

        public List<int> ClientsPrefabcount;


        public AudioSource Sound_Background;
        public AudioSource Sound_Win;
        public AudioSource Sound_Lose;
        public AudioSource Sound_Money;

        public List<Text> label_level;
        public Text label_money;

        
        void Start()
        {
            LoadGame();
            UIController = GameObject.Find("Canvas").GetComponent<GameSceneUIController>();
            ClientsHolder = GameObject.Find("ClientsHolder");
            EnvironmentHolder = GameObject.Find("EnvironmentHolder");
            
            EnvironmentHolder.SetActive(false);
            IsGamePaused = false;
            data = LevelInfo.GetLevel(PlayerData.levelToLoad);
            UIController.UpdateClientCounter(0, data.ClientCount);
            UIController.UpdateCoinCounter(0);
            levelTimer = (float)data.Time.TotalSeconds;
            UIController.UpdateLevelTimer(levelTimer);
            
        }

        void Update()
        {
            if (State != GameState.Brief)
            {
                if (levelTimer == -1)
                {

                }
                else
                {
                    if (levelTimer > 0)
                    {
                        levelTimer -= Time.deltaTime;
                        UIController.UpdateLevelTimer(levelTimer);
                    }
                    else
                    {
                        State = GameState.PreLose;
                    }
                }
                
                
                
            }
            //Debug.Log(State);
            switch (State)
            {
                case GameState.Brief:
                    if (!UIController.IsBrief)
                    {
                        EnvironmentHolder.SetActive(true);
                        Sound_Background.Play();
                        State = GameState.Idle;
                    }
                    break;
                case GameState.ClientComing:
                    if (CurrentClient.state == ClientState.IDLE)
                    {
                        State = GameState.Idle;
                    }
                    break;
                case GameState.ClientLeaving:
                    if (CurrentClient.state == ClientState.GONE)
                    {
                        PlayerData.CoinCounter += CurrentClient.GetMoney();
                        Sound_Money.Play();
                        UIController.UpdateCoinCounter(PlayerData.CoinCounter);
                        Destroy(CurrentClient.gameObject);
                        GameObject.Find("GiftTable").GetComponent<MaterialManager>().PresentColor = Color.white;
                        GameObject.Find("GiftTable").GetComponent<MaterialManager>().RibbonColor = Color.white;
                        CurrentClient = null;
                        State = GameState.Idle;

                    }
                    break;
                case GameState.Idle:
                    
                    if (CurrentClient == null && ClientIdx == data.ClientCount - 1)
                    {
                        State = GameState.PreWon;
                        break;
                    }
                    if (CurrentClient == null)
                    {
                        createClient();
                        State = GameState.ClientComing;
                    }
                    else
                    {
                        if (CurrentClient.isSatisfied(CurPresentColor, CurRibbonColor))
                        {
                            CurrentClient.GoOut();
                            State = GameState.ClientLeaving;
                            CurPresentColor = PresentColor.White;
                            CurRibbonColor = RibbonColor.White;
                        }
                    }
                    break;
                case GameState.PreWon:
                    // show win window
                    label_level[0].text = $"Level {PlayerData.levelToLoad+1}";
                    label_money.text = $"{ PlayerData.CoinCounter}";
                    PlayerData.CoinCounterStore = PlayerData.CoinCounterStore + PlayerData.CoinCounter;
                    PlayerData.levelToLoad += 1;
                    SaveGame();
                    Sound_Background.Stop();
                    Sound_Win.Play();
                    levelTimer = -1;
                    UIController.OnWin();
                    State = GameState.Won;
                    break;
                case GameState.Won:
                    break;
                case GameState.PreLose:
                    label_level[1].text = $"Level {PlayerData.levelToLoad+1}";
                    
                    
                    levelTimer = -1;
                    Sound_Background.Stop();
                    Sound_Lose.Play();
                    UIController.OnLose();
                    State = GameState.Lose;
                    break;
                case GameState.Lose:
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }
        
        private void createClient()
        {
            int a = UnityEngine.Random.Range(0, 8);
            GameObject client = Instantiate(ClientsPrefab[a]);
            CurrentClient = client.GetComponent<ClientController>();
            CurrentClient.clientIdx = ++ClientIdx;
            client.transform.SetParent(ClientsHolder.transform, true);
            UIController.UpdateClientCounter(ClientIdx + 1, data.ClientCount);
        }

        private void OnResume()
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        private void OnPause()
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
        }

        public void TogglePause()
        {
            if (IsGamePaused) {
                OnResume();
            } else {
                OnPause();
            }
        }
        private SaveData CreateSaveGameObject()
        {
            SaveData save = new SaveData
            {
                levelToLoad = PlayerData.levelToLoad,
                CoinCounterStore = PlayerData.CoinCounterStore,
                AvailableRibbonColors = PlayerData.AvailableRibbonColors,
                AvailablePresentColor = PlayerData.AvailablePresentColor,
                BoughtRibbonColorStr = PlayerData.BoughtRibbonColorStr,
                BoughtPaperColorStr = PlayerData.BoughtPaperColorStr,
                
            };
            return save;
        }
        public void SaveGame()
        {

            SaveData save = CreateSaveGameObject();

            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);
            file.Close();
            Debug.Log("Game Saved");
        }
        public static void LoadGame()
        {
            
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
               
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                SaveData save = (SaveData)bf.Deserialize(file);
                file.Close();

                PlayerData.levelToLoad = save.levelToLoad;
                PlayerData.CoinCounterStore = save.CoinCounterStore;
                PlayerData.AvailableRibbonColors = save.AvailableRibbonColors;
                

                PlayerData.AvailablePresentColor = save.AvailablePresentColor;
                PlayerData.BoughtRibbonColorStr = save.BoughtRibbonColorStr;
                PlayerData.BoughtPaperColorStr = save.BoughtPaperColorStr;

                Debug.Log("Game Loaded");
                
            }
            else
            {
                Debug.Log("No game saved!");
            }
        }
        
    }
}
