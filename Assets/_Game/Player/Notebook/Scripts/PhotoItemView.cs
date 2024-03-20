using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PhotoItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;

    [SerializeField] private GameObject _frontPhoto;
    [SerializeField] private GameObject _backPhoto;

    public void DisplayFrontPhoto(Texture2D photo)
    {
        _frontPhoto.SetActive(true);
        _backPhoto.SetActive(false);
        _frontPhoto.GetComponent<RawImage>().texture = photo;
    }

    public void DisplayBackPhoto(string description)
    {
        _backPhoto.SetActive(true);
        _frontPhoto.SetActive(false);
        _description.text = description;
    }
}
