using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private string _nextScene;
    [SerializeField] public List<MonoBehaviour> _managersToLoad;
    
    void Awake()
    {
        var panel = Instantiate(_loadingPanel).GetComponent<LoadingPanelController>();
        panel.SetSceneToLoad(_nextScene);

        var gameObjectWithManagers = Instantiate(new GameObject());
        gameObjectWithManagers.name = "ManagersContainer";
        DontDestroyOnLoad(gameObjectWithManagers);

       /*
        foreach (var manager in _managersToLoad)
        {
            gameObjectWithManagers.AddComponent(manager.GetType());
        }
       */
    }
}