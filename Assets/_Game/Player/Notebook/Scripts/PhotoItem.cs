using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhotoItem : MonoBehaviour
{
    [SerializeField] private PhotoItemView _photoItemView;

    private PhotoConfig _noteConfig;
    private bool _currentSide;
    public void Initialized(PhotoConfig noteConfig)
    {
        _noteConfig = noteConfig;
        _photoItemView.DisplayFrontPhoto(_noteConfig.Photo);
        _currentSide = true;
    }

    public void Flip()
    {
        _currentSide = !_currentSide;
        if (_currentSide) _photoItemView.DisplayFrontPhoto(_noteConfig.Photo);
        else _photoItemView.DisplayBackPhoto(_noteConfig.Description);
    }

    public void EnableHiddenText(string hiddenText)
    {
        _photoItemView.EnableHiddenText(hiddenText);
    }
}
