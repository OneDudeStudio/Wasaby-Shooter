using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    public void SelectButton(int num)
    {
        int div = num / 4;
        for(int i = div * 4; i < div * 4 + 4; i++)
        {
            _buttons[i].image.color = Color.white;
            _buttons[num].image.color = new Color(0f, 255/255f, 78/255f);
        }
    }
}
