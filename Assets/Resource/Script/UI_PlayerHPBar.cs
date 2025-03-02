using UnityEngine;

public class UI_PlayerHPBar : MonoBehaviour
{
    public GameObject[] HpImages;
    
    public void Set(int value)
    {
        for (int i = 0; i < HpImages.Length; i++)
        {
            HpImages[i].SetActive(true);
        }
        for (int i = 0; i < HpImages.Length - value; i++)
        {
            HpImages[i].SetActive(false);
        }
    }
}
