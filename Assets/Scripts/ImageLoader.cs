using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using DG.Tweening;

public class ImageLoader : MonoBehaviour
{
    public Sprite imageBack;
    private Sprite _imageFace;
    private bool _imageIsFaceUp;
    private Image _image; // image where we download our pic to
    private string _imageLink = "https://picsum.photos/200/300"; // where we take our pictures from
    private Sequence _turningImageSequence; //To animate image-turning

    public string ImageLink
    {
        get { return _imageLink; }
    }

    private void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
        _image.sprite = imageBack;
        _imageIsFaceUp = false;
    }

    /// <summary>
    /// Animate image-turning
    /// </summary>
    /// <param name="timeToTurnImage"></param>
    public void TurnImageInSequence(float timeToTurnImage)
    {
        _turningImageSequence = DOTween.Sequence();
        if(_imageIsFaceUp)
        {
            _turningImageSequence.Append(transform.DORotate(new Vector3(0, -90, 0), timeToTurnImage / 2, RotateMode.Fast));
            _turningImageSequence.AppendCallback(() => AssignNewImageToImage(imageBack));
        }
        _turningImageSequence.Append(transform.DORotate(new Vector3(0, -180, 0), timeToTurnImage / 2, RotateMode.Fast));
        _turningImageSequence.Append(transform.DORotate(new Vector3(0, -90, 0), timeToTurnImage / 2, RotateMode.Fast));
        _turningImageSequence.AppendCallback(() => AssignNewImageToImage(_imageFace));
        _turningImageSequence.Append(transform.DORotate(new Vector3(0, 0, 0), timeToTurnImage / 2, RotateMode.Fast));
        _turningImageSequence.Play();
    }

    /// <summary>
    /// Assign new sprites in animation
    /// </summary>
    /// <param name="spriteToAssign"></param>
    private void AssignNewImageToImage(Sprite spriteToAssign)
    {
        _image.sprite = spriteToAssign;
        _imageIsFaceUp = true;
    }

    /// <summary>
    /// Load image from website as it may
    /// </summary>
    public async void LoadImage()
    {
        await AsyncLoadImage(ImageLink);
        TurnImageInSequence(0.6f);
    }

    /// <summary>
    /// Download picture from the webside with its ImageLink
    /// </summary>
    /// <param name="ImageLink"></param>
    /// <returns></returns>
    public async Task AsyncLoadImage(string ImageLink)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(ImageLink);
        request.SendWebRequest();
        while (!request.isDone) //wait till image is downloaded from the link
        {
            await Task.Yield();
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("No error");
            Texture2D randomTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite randomSprite = Sprite.Create(randomTexture, new Rect(0, 0, randomTexture.width, randomTexture.height), new Vector2(0.5f, 0.5f));
            _imageFace = randomSprite;
        }
    }
}
