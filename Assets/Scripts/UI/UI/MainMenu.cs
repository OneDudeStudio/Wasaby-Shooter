using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private const int GameSceneIndex = 1;
        
        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
        
        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(GameSceneIndex);
        }
        
        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}