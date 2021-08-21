using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwap : MonoBehaviour
{
    public Sprite OffSprite;
    public Sprite OnSprite;
    public Button button;
    public void ChangeImage()
    {
        if (button.image.sprite == OnSprite)
            button.image.sprite = OffSprite;
        else
        {
            button.image.sprite = OnSprite;
        }
    }
}
