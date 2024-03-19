using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoItemView : MonoBehaviour
{
    [SerializeField] private Image _photo;

    public void DisplayPhoto(Sprite photo)
    {
        _photo.sprite = photo;
    }
}
