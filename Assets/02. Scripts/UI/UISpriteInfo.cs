using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpriteInfo : MonoBehaviour
{
    public UISprite targetSprite;

    void Start()
    {
        if (targetSprite == null)
        {
            Debug.LogError("Target UISprite is not assigned!");
            return;
        }

        Debug.Log("UISprite Debug Information:");
        Debug.Log("Local Scale: " + targetSprite.cachedTransform.localScale);
        Debug.Log("Atlas Pixel Size: " + targetSprite.atlas.pixelSize);
        
        UIAtlas.Sprite spriteInfo = targetSprite.atlas.GetSprite(targetSprite.spriteName);
        if (spriteInfo != null)
        {
            Debug.Log("Sprite Outer Rect: " + spriteInfo.outer);
            Debug.Log("Sprite Inner Rect: " + spriteInfo.inner);
        }
        
        /*
        Debug.Log("Padding Left: " + targetSprite.paddingLeft);
        Debug.Log("Padding Right: " + targetSprite.paddingRight);
        Debug.Log("Padding Top: " + targetSprite.paddingTop);
        Debug.Log("Padding Bottom: " + targetSprite.paddingBottom);
        */
        
        Debug.Log("Border: " + targetSprite.border);
        
        // 'localSize' might not be available in this version, but if it is, uncomment the next line
        // Debug.Log("Local Size: " + targetSprite.localSize);
        
        // Calculate approximate width and height
        float spriteWidth = (spriteInfo.outer.width / targetSprite.atlas.pixelSize) * targetSprite.cachedTransform.localScale.x;
        float spriteHeight = (spriteInfo.outer.height / targetSprite.atlas.pixelSize) * targetSprite.cachedTransform.localScale.y;
        Debug.Log("Approximate Width: " + spriteWidth);
        Debug.Log("Approximate Height: " + spriteHeight);
    }
}
