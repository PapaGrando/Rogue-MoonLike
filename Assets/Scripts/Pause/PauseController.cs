using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static bool Paused = false;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioClip _buttonPressSound;

    private Animator _anim;
    private Camera _camera;

    void Awake()
    {
        if (Paused)
        {
            Destroy(gameObject);
            Debug.LogWarning("Pause already created, new instance will destroyed. Check PauseController.Paused before instancing");
        }

        _anim = GetComponent<Animator>();
        _camera = Camera.main;
        _soundSlider.value = AudioListener.volume;

        Paused = true;
    }

    public void OnSoundSliderValueChanged()
    {
        AudioListener.volume = _soundSlider.value;
    }

    public void OnResumePressed()
    {
        _anim.SetTrigger("Resume");
        if (_buttonPressSound != null) AudioSource.PlayClipAtPoint(_buttonPressSound, _camera.transform.position, 1f);
    }

    public void OnQuitPressed()
    {
        _anim.SetTrigger("Quit");
        if (_buttonPressSound != null) AudioSource.PlayClipAtPoint(_buttonPressSound, _camera.transform.position, 1f);
    }

    public void OnResumeAnimationEnded()
    {
        Paused = false;
        Destroy(gameObject);
    }

    public void OnQuitAnimationEnded()
    {
        Paused = false;
        Application.Quit();
    }
}