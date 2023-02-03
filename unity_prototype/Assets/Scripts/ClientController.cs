using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public enum ClientState
    {
        WALK_IN,
        IDLE,
        WALK_OUT,
        GONE,
    }

    public class ClientController : MonoBehaviour
    {
        //private float startX = -3f;
        //private float endX = 6f;
        private float walkTime = 0.5f; //time moving
        private float walkTimer;
        public Client data; //data of client
        public ClientState state = ClientState.WALK_IN;
        public int clientIdx;
        GameObject Bubble; // Bubble where is gift of client

        void Start()
        {
            data = LevelInfo.GetLevel(PlayerData.levelToLoad).Clients[clientIdx];
            //var spr = GameObject.Find("ClientSpr").GetComponent<SpriteRenderer>();
            Bubble = GameObject.Find("Bubble");
            Bubble.SetActive(false);
            var gift = gameObject.transform.Find("Bubble/GiftModel");
            var cmp = gift.GetComponent<MaterialManager>();
            cmp.Init();
            cmp.WrapColor = data.PresentColor.ToColor();
            cmp.RibbonColor = data.RibbonColor.ToColor();
            transform.position = new Vector3(0f, 0f, 0f);
            transform.localPosition = new Vector3(-3f, 0f, 0f);
            walkTimer = walkTime;
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case ClientState.WALK_IN:
                    if (walkTimer > 0)
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, 0f, 0f), walkTime - walkTimer);
                        walkTimer -= Time.deltaTime;
                        return;
                    }
                    state = ClientState.IDLE;
                    Bubble.SetActive(true);
                    break;
                case ClientState.IDLE:
                    break;
                case ClientState.WALK_OUT:
                    if (walkTimer > 0)
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(6f, 0f, 0f), walkTime - walkTimer);
                        walkTimer -= Time.deltaTime;
                        return;
                    }
                    state = ClientState.GONE;
                    break;
                case ClientState.GONE:
                    break;
            }
        }

        public bool isSatisfied(PresentColor presentColor, RibbonColor ribbonColor)
        {
            return presentColor == data.PresentColor && ribbonColor == data.RibbonColor;
        }

        public void GoOut()
        {
            state = ClientState.WALK_OUT;
            walkTimer = walkTime;
        }

        public int GetMoney()
        {
            return 5;
        }
    }
}
