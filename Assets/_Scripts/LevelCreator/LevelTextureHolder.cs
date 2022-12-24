using System.Collections;
using System.Collections.Generic;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextureHolder : Singleton<LevelTextureHolder>
{
    [SerializeField] private RawImage textureHolder;
    [SerializeField] private RectTransform rTransfrom;

    public void SetTexture(Texture2D texture)
    {
        float newHeight, newWidth;
        float aspect = texture.width / (float)texture.height;

        bool imageIsWide = texture.width > texture.height;

        if (imageIsWide)
        {
            newWidth = (int)rTransfrom.rect.width;
            newHeight = Mathf.RoundToInt(newWidth / aspect);
        }
        else
        {
            newHeight = (int)rTransfrom.rect.height;
            newWidth = newHeight * aspect;
        }

        rTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        rTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

        textureHolder.texture = texture;
    }
}
