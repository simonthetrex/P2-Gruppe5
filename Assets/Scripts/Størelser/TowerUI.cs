using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public Image image;

    public void Setup(Tårne data)
    {
        image.sprite = data.towerSprite;
    }
}