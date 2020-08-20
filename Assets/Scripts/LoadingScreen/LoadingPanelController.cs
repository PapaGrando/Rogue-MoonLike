using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelController : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private Image _square;
    [SerializeField] private Text _progressText;
    [SerializeField] private Text _loadingText;
    [Space]
    [SerializeField] private float _smoothIncrease;
    [SerializeField] private string _targetSceneName;

    private Animator _anim;
    private AsyncOperation _asyncLoading;
    
    private bool _readyToLoading;
    private float _progress = 0;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);

        if(_targetSceneName != "")
            SetSceneToLoad(_targetSceneName);
    }

    void Update()
    {
        if (_readyToLoading)
        {
            if (_progress >= 1.0f)
                StartExitAnim();

            // при allowSceneActivation = false прогресс доходит только до 0.9
            // для дальнейшей загрузки надо выставить true
            if (_progress >= 0.9f)
                UpdateProgress(1.0f);
            else
                UpdateProgress(_asyncLoading.progress);


            ShowProgress(_progress);
        }

        void UpdateProgress(float max) => _progress = Mathf.Clamp(_progress + _smoothIncrease * Time.deltaTime, 0, max);
    }

    void StartExitAnim()
    {
        // окончательная итерация загрузки и активация анимации выхода
        _asyncLoading.allowSceneActivation = true;

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        _asyncLoading.completed += x =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_targetSceneName));
            _anim.SetTrigger("SceneLoaded");
        };
    }

    private void StartLoading()
    {
        _asyncLoading = SceneManager.LoadSceneAsync(_targetSceneName);
        _asyncLoading.allowSceneActivation = false;
    }

    private void ShowProgress(float progress)
    {
        _square.rectTransform.localScale = new Vector3(10 * progress, 10 * progress, 1);
        _progressText.text = $"{Mathf.Round(_progress * 100)}%";
    }

    //события из анимации
    public void OnScreenOutAnimationEnded() => Destroy(gameObject);

    public void OnScreenInAnimationEnded()
    {
        if (_targetSceneName != "")
            _readyToLoading = true;
        StartLoading();
    }

    public void SetSceneToLoad(string val)
    {
        _targetSceneName = val;
        if (_targetSceneName != "")
            _loadingText.text = "LOADING";
    } 
}
