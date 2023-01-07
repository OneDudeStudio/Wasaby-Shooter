using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text _loadingPercentage;
    [SerializeField] private Image _loadingProgressBar;

    private static SceneTransition _instanceSceneTransition;
    private static bool _shouldPlayOpeningAnimation = false;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        _instanceSceneTransition._componentAnimator.SetTrigger("SceneClosing");

        _instanceSceneTransition._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        // disabling automatic scene switching
        _instanceSceneTransition._loadingSceneOperation.allowSceneActivation = false;

        _instanceSceneTransition._loadingProgressBar.fillAmount = 0;
    }

    private void Start()
    {
        _instanceSceneTransition = this;
        _componentAnimator = GetComponent<Animator>();

        if (_shouldPlayOpeningAnimation)
        {
            _componentAnimator.SetTrigger("SceneOpening");
            _instanceSceneTransition._loadingProgressBar.fillAmount = 1;
            _shouldPlayOpeningAnimation = false;
        }
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _loadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";
            _loadingProgressBar.fillAmount = _loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnimation = true;
        _loadingSceneOperation.allowSceneActivation = true;
    }
}