using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StoreUIController : MonoBehaviour
{
    public GameObject StoreUI;
    public GameObject ScrollViewRibbon;
    public ScrollRect scrollRectRibbons;
    public ScrollRect scrollRectRibbons1;
    public GameObject btn_empty_ribbon;

    public GameObject ScrollViewPaper;
    public ScrollRect scrollRectPapers;
    public ScrollRect scrollRectPapers1;
    public GameObject btn_empty_paper;
   
    public Text MainCoins;
    public GameObject btn_next;
    public GameObject btn_prev;
    
    public Scrollbar scrollBarRibbons;
    public Scrollbar scrollBarPapers;


    public GameObject btn_paper;
    public GameObject btn_ribbon;

    public ScrollRect RibbonScrollView;
    public ScrollRect PresentScrollView;
    private int Steps = 9;

    private List<GameObject> Buttons = new List<GameObject>();
   
    void Start()
    {

        GameSceneController.LoadGame();
        initScrollBarRibbons();
        initScrollBarRibbons1();
        initScrollBarPapers();
        initScrollBarPapers1();
        initScrollBarRibbons1Bought();
        initScrollBarPaperBought();
        initScrollBarPaper1Bought();
        MainCoins.text = PlayerData.CoinCounterStore.ToString();

        initscrollRectRibbonshorizontalzero();
        initscrollRectPapershorizontalzero();
        inithorizontalScrollbarnumberOfSteps();


        btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = false;
        btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    private void initScrollBarRibbons()
    {
        var content = scrollRectRibbons.content;
        
        foreach (var color in PlayerData.AvailableNewRibbonColors)
        {
    
            if (color == RibbonColor.newRibbonColor)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject btn = Instantiate(btn_empty_ribbon);
                    btn.transform.Find("ribbonColor").gameObject.SetActive(false);
                    Buttons.Add(btn);
                    var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                    btn.transform.SetParent(content, false);
                    btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    coinLbl.GetComponent<Text>().text = "0";
                }
            }
            else
            {
                GameObject btn = Instantiate(btn_empty_ribbon);
                Buttons.Add(btn);
                var ribbon = btn.transform.Find("ribbonColor");
                var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                btn.transform.SetParent(content, false);
                ribbon.GetComponent<UnityEngine.UI.Image>().color = color.ToColor();
                btn.name = color.ToString();
                coinLbl.GetComponent<Text>().text = color.ToInt().ToString();
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BuyNewRibbon(color, btn, coinLbl));
            }
        }
    }
    private void initScrollBarRibbons1()
    {

        var content = scrollRectRibbons1.content;
        for (int i = 0; i < 9; i++)
        {
            GameObject btn = Instantiate(btn_empty_ribbon);
            btn.transform.Find("ribbonColor").gameObject.SetActive(false);
            Buttons.Add(btn);
            var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
            btn.transform.SetParent(content, false);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
            coinLbl.GetComponent<Text>().text = "0";
        }
    }
    public void BuyNewRibbon(RibbonColor color, GameObject btn, Transform coinLbl)
    {
        if (PlayerData.CoinCounterStore >= color.ToInt())
        {
            PlayerData.AvailableRibbonColors.Add(color);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
            PlayerData.CoinCounterStore = PlayerData.CoinCounterStore - color.ToInt();
            MainCoins.text = PlayerData.CoinCounterStore.ToString();
            coinLbl.GetComponent<Text>().text = "OK";
            coinLbl.GetComponent<Text>().color = Color.green;
            PlayerData.BoughtRibbonColorStr.Add(btn.name);
        }
    }

    private void initScrollBarRibbons1Bought()
    {
        
        if (PlayerData.BoughtRibbonColorStr.Count !=0)
        {
            for (int i = 0; i < scrollRectRibbons.content.childCount; i++)
            {
                foreach (var btnbought in PlayerData.BoughtRibbonColorStr)
                {
                    Debug.Log("Name " + scrollRectRibbons.content.GetChild(i).name);
                    Debug.Log("Name1 " + btnbought);
                    if (scrollRectRibbons.content.GetChild(i).name == btnbought)
                    {
                        scrollRectRibbons.content.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;
                        var coinLbl = scrollRectRibbons.content.GetChild(i).transform.Find("CoinPanel").transform.Find("CoinLbl");
                        coinLbl.GetComponent<Text>().text = "OK";
                        coinLbl.GetComponent<Text>().color = Color.green;
                    }

                }
            }
        }
    }

    private void initScrollBarPaperBought()
    {
        if (PlayerData.BoughtPaperColorStr.Count != 0)
        {
            for (int i = 0; i < scrollRectPapers.content.childCount; i++)
            {
                foreach (var btnbought in PlayerData.BoughtPaperColorStr)
                {
                    if (scrollRectPapers.content.GetChild(i).name == btnbought)
                    {
                        scrollRectPapers.content.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;
                        var coinLbl = scrollRectPapers.content.GetChild(i).transform.Find("CoinPanel").transform.Find("CoinLbl");
                        coinLbl.GetComponent<Text>().text = "OK";
                        coinLbl.GetComponent<Text>().color = Color.green;
                    }
                }
            }
        }
    }
    
    private void initScrollBarPaper1Bought()
    {
        if (PlayerData.BoughtPaperColorStr.Count != 0)
        {
            for (int i = 0; i < scrollRectPapers1.content.childCount; i++)
            {
                foreach (var btnbought in PlayerData.BoughtPaperColorStr)
                {
                    Debug.Log("Name " + scrollRectRibbons.content.GetChild(i).name);
                    Debug.Log("Name1 " + btnbought);
                    if (scrollRectPapers1.content.GetChild(i).name == btnbought)
                    {
                        scrollRectPapers1.content.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;
                        var coinLbl = scrollRectPapers1.content.GetChild(i).transform.Find("CoinPanel").transform.Find("CoinLbl");
                        coinLbl.GetComponent<Text>().text = "OK";
                        coinLbl.GetComponent<Text>().color = Color.green;
                    }

                }
            }
        }
    }
    
    private void initScrollBarPapers()
    {
        
        var content = scrollRectPapers.content;
        foreach (var color in PlayerData.AvailableNewPaperColors1)
        {
            if (color == PresentColor.newPaperColor)
            {
                for (int i = 0; i < 6; i++) 
                {
                    GameObject btn = Instantiate(btn_empty_paper);
                    btn.transform.Find("paperColor").gameObject.SetActive(false);
                    Buttons.Add(btn);
                    var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                    btn.transform.SetParent(content, false);
                    btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    coinLbl.GetComponent<Text>().text = "0";
                }

            }
            else
            {
                GameObject btn = Instantiate(btn_empty_paper);
                Buttons.Add(btn);
                var ribbon = btn.transform.Find("paperColor");
                var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                btn.transform.SetParent(content, false);
                ribbon.GetComponent<UnityEngine.UI.Image>().color = color.ToColor();
                btn.name = color.ToString();
                coinLbl.GetComponent<Text>().text = color.ToInt().ToString();
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BuyNewPaper(color, btn, coinLbl));
            }
        }
    }
    
    private void initScrollBarPapers1()
    {
        var content = scrollRectPapers1.content;
        foreach (var color in PlayerData.AvailableNewPaperColors2)
        {
            if (color == PresentColor.newPaperColor)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject btn = Instantiate(btn_empty_paper);
                    btn.transform.Find("paperColor").gameObject.SetActive(false);
                    Buttons.Add(btn);
                    var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                    btn.transform.SetParent(content, false);
                    btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    coinLbl.GetComponent<Text>().text = "0";
                }
            }
            else
            {
                GameObject btn = Instantiate(btn_empty_paper);
                Buttons.Add(btn);
                var ribbon = btn.transform.Find("paperColor");
                var coinLbl = btn.transform.Find("CoinPanel").transform.Find("CoinLbl");
                btn.transform.SetParent(content, false);
                ribbon.GetComponent<UnityEngine.UI.Image>().color = color.ToColor();
                btn.name = color.ToString();
                coinLbl.GetComponent<Text>().text = color.ToInt().ToString();
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BuyNewPaper(color, btn, coinLbl));
            }
        }
    }
    
    public void BuyNewPaper(PresentColor color, GameObject btn, Transform coinLbl)
    {
        if (PlayerData.CoinCounterStore >= color.ToInt())
        {
            PlayerData.AvailablePresentColor.Add(color);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
            PlayerData.CoinCounterStore = PlayerData.CoinCounterStore - color.ToInt();
            MainCoins.text = PlayerData.CoinCounterStore.ToString();
            coinLbl.GetComponent<Text>().text = "OK";
            coinLbl.GetComponent<Text>().color = Color.green;
            PlayerData.BoughtPaperColorStr.Add(btn.name);
        }   
    }
    
    private void initscrollRectRibbonshorizontalzero()
    {
        scrollRectRibbons.horizontalScrollbar.value = 0;
        scrollRectRibbons.horizontalNormalizedPosition = 0;
        scrollRectRibbons1.horizontalScrollbar.value = 0;
        scrollRectRibbons1.horizontalNormalizedPosition = 0;
    }

    private void initscrollRectPapershorizontalzero()
    {
        scrollRectPapers.horizontalScrollbar.value = 0;
        scrollRectPapers.horizontalNormalizedPosition = 0;
        scrollRectPapers1.horizontalScrollbar.value = 0;
        scrollRectPapers1.horizontalNormalizedPosition = 0;
    }
    private void inithorizontalScrollbarnumberOfSteps()
    {
        scrollRectRibbons.horizontalScrollbar.numberOfSteps = Steps / 3;
        scrollRectRibbons1.horizontalScrollbar.numberOfSteps = Steps / 3;
        scrollRectPapers.horizontalScrollbar.numberOfSteps = Steps / 3;
        scrollRectPapers1.horizontalScrollbar.numberOfSteps = Steps / 3;
    }

    public void OnEnableRibbons()
    {
        scrollBarRibbons.onValueChanged.AddListener(delegate { changescrollRectRibbonsValued(scrollRectRibbons.horizontalScrollbar.value); });
    }

    public void changescrollRectRibbonsValued(float horizontalScrollbarvalue)
    {
        if ((horizontalScrollbarvalue > 0) && (horizontalScrollbarvalue <1))
        {
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = true;
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        if (horizontalScrollbarvalue == 0)
        {
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = false;
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        if (horizontalScrollbarvalue == 1)
        {
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = false;
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }
    public void OnEnablePapers()
    {
        scrollBarPapers.onValueChanged.AddListener(delegate { changescrollRectPapersValued(scrollRectPapers.horizontalScrollbar.value); });
    }

    public void changescrollRectPapersValued(float horizontalScrollbarvalue)
    {
        if ((horizontalScrollbarvalue > 0) && (horizontalScrollbarvalue < 1))
        {
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = true;
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        if (horizontalScrollbarvalue == 0)
        {
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = false;
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        if (horizontalScrollbarvalue == 1)
        {
            btn_next.GetComponent<UnityEngine.UI.Button>().interactable = false;
            btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }

    public void Btn_scrollingRight()
    {
        if (ScrollViewRibbon.activeSelf)
        {
            if (scrollRectRibbons.horizontalScrollbar.value != 1)
            {
                scrollRectRibbons.horizontalScrollbar.value += scrollRectRibbons.horizontalScrollbar.size;
                scrollRectRibbons1.horizontalScrollbar.value += scrollRectRibbons1.horizontalScrollbar.size;
            }
                
        }

        if (ScrollViewPaper.activeSelf)
        {
            if (scrollRectPapers.horizontalScrollbar.value != 1)
            {
                scrollRectPapers.horizontalScrollbar.value += scrollRectPapers.horizontalScrollbar.size;
                scrollRectPapers1.horizontalScrollbar.value += scrollRectPapers1.horizontalScrollbar.size;
            }
        }
    }
    
    public void Btn_scrollingleft()
    {
        if (ScrollViewPaper.activeSelf)
        {
            if (scrollRectPapers.horizontalScrollbar.value != 0)
            {
                scrollRectPapers.horizontalScrollbar.value -= scrollRectPapers.horizontalScrollbar.size;
                scrollRectPapers1.horizontalScrollbar.value -= scrollRectPapers1.horizontalScrollbar.size;
            }
        }
        if (ScrollViewRibbon.activeSelf)
        {
            if (scrollRectRibbons.horizontalScrollbar.value != 0)
            {
                scrollRectRibbons.horizontalScrollbar.value -= scrollRectRibbons.horizontalScrollbar.size;
                scrollRectRibbons1.horizontalScrollbar.value -= scrollRectRibbons1.horizontalScrollbar.size;
            }
        }
    }
    
    public void Btn_paper()
    {
        ScrollViewRibbon.SetActive(false);
        ScrollViewPaper.SetActive(true);
        initscrollRectPapershorizontalzero();
        btn_paper.GetComponent<UnityEngine.UI.Button>().interactable = false;
        btn_ribbon.GetComponent<UnityEngine.UI.Button>().interactable = true;
        btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
    
    public void Btn_ribbon()
    {
        ScrollViewPaper.SetActive(false);
        ScrollViewRibbon.SetActive(true);
        initscrollRectRibbonshorizontalzero();
        btn_ribbon.GetComponent<UnityEngine.UI.Button>().interactable = false;
        btn_paper.GetComponent<UnityEngine.UI.Button>().interactable = true;
        btn_next.GetComponent<UnityEngine.UI.Button>().interactable = true;
        btn_prev.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}