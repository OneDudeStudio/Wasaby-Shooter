using UnityEngine;

namespace UI.UI
{
    public class CanvasPanel : MonoBehaviour
    {

        public void ActivatePanel()
        {
            gameObject.SetActive(true);
        }
        
        public void DeactivatePanel()
        {
            gameObject.SetActive(false);
        }
        
    }
}
