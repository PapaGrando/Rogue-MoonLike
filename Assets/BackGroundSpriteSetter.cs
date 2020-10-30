using UnityEngine;

public class BackGroundSpriteSetter : MonoBehaviour
{
    public void SetSprites(Sprite[] sprites)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var layerTransform = transform.GetChild(i).transform;
            foreach (var child in layerTransform)
            {
                GetComponent<SpriteRenderer>().sprite = sprites.Length - 1 <= i ? null : sprites[i];
            }
        }
    }
}
