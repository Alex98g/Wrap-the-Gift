using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioScript : MonoBehaviour
{
    public class ScreenSize
    {
        public static float GetScreenToWorldHeight
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var height = edgeVector.y * 2;
                return height;
            }
        }
        public static float GetScreenToWorldWidth
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var width = edgeVector.x * 2;
                return width;
            }
        }
    }
    public SpriteRenderer RedSprite, GreenSprite;
    float screenWidth;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = ScreenSize.GetScreenToWorldWidth;
        //update scale of Red sprite to half of screen' width
        RedSprite.transform.localScale = new Vector3(screenWidth / 2, screenWidth / 2, screenWidth / 2);
        //update position of Red sprite to fit in the left half of the screen
        RedSprite.transform.position = new Vector3(-(screenWidth / 2) / 2, RedSprite.transform.position.y, RedSprite.transform.position.z);
        //update scale of Green sprite to half of screen' width
        GreenSprite.transform.localScale = new Vector3(screenWidth / 2, screenWidth / 2, screenWidth / 2);
        //update position of Green sprite to fit in the right half of the screen
        GreenSprite.transform.position = new Vector3((screenWidth / 2) / 2, GreenSprite.transform.position.y, GreenSprite.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
