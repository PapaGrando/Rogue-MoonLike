using UnityEngine;

public class PauseActivator : MonoBehaviour
{
    [SerializeField] private GameObject _pauseWindowPrefab;

    public void OnButtonPressed()
    {
        if (!PauseController.Paused) Instantiate(_pauseWindowPrefab);
    }
}