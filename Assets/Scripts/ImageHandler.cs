using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// All the possible states in dropdown menu
/// </summary>
public enum ImageLoaderState
{
    allAtOnce,
    oneByOne,
    whenImageReady
}

/// <summary>
/// Governs images logic
/// </summary>
public class ImageHandler : MonoBehaviour
{
    public ImageLoaderState imageLoaderState;
    [SerializeField] private ImageLoader[] _imageLoaders;
    private bool _isLoadingImages; //to cancel image-loading at will

    private void Awake() => _isLoadingImages = true;

    public void CancelLoadingImages()
    {
        _isLoadingImages = false;
    }

    public async void LoadImages()
    {
        _isLoadingImages = true;
        switch (imageLoaderState)
        {
            case ImageLoaderState.allAtOnce:
                for(int i = 0; i < _imageLoaders.Length; i++)
                {
                    if(_isLoadingImages)
                    {
                        await _imageLoaders[i].AsyncLoadImage(_imageLoaders[i].ImageLink);
                    }
                }
                for (int i = 0; i < _imageLoaders.Length; i++)
                {
                    _imageLoaders[i].TurnImageInSequence(0.6f);
                }
                break;

            case ImageLoaderState.oneByOne:
                for (int i = 0; i < _imageLoaders.Length; i++)
                {
                    if (_isLoadingImages)
                    {
                        await _imageLoaders[i].AsyncLoadImage(_imageLoaders[i].ImageLink);
                        _imageLoaders[i].TurnImageInSequence(0.6f);
                    }
                }
                break;

            case ImageLoaderState.whenImageReady:
                for (int i = 0; i < _imageLoaders.Length; i++)
                {
                    if (_isLoadingImages)
                    {
                        _imageLoaders[i].LoadImage();
                    }    
                }
                break;
        }
    }
}
