using IngameDebugConsole;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private string _nextScene = "Game";
    [Header("Managers")] 
    [SerializeField] private DebugLogManager _debugConsoleUI;
    
    void Awake()
    {
        var panel = Instantiate(_loadingPanel).GetComponent<LoadingPanelController>();
        panel.SetSceneToLoad(_nextScene);

        var gameObjectWithManagers = Instantiate(new GameObject());

        gameObjectWithManagers.name = "ManagersContainer";
        gameObjectWithManagers.tag = "ManagersContainer";
        DontDestroyOnLoad(gameObjectWithManagers);

#if DEBUG && !UNITY_EDITOR
        Instantiate(_debugConsoleUI, gameObjectWithManagers.transform);
#endif
        /*
         foreach (var manager in _managersToLoad)
         {
             gameObjectWithManagers.AddComponent(manager.GetType());
         }
        */
    }
}