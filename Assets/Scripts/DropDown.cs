using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    [SerializeField] private ImageHandler _imageHandler; //to send info to about chosen in dropmenu state

    private void SetImageLoaderState(ImageLoaderState imageLoaderState)
    {
            _imageHandler.imageLoaderState = imageLoaderState;
    }

    public void HandleInputData(int value)
    {
        switch (value)
        {
            case 0:
                SetImageLoaderState(ImageLoaderState.allAtOnce);
                break;
            case 1:
                SetImageLoaderState(ImageLoaderState.oneByOne);
                break;
            case 2:
                SetImageLoaderState(ImageLoaderState.whenImageReady);
                break;
        }
    }
}
