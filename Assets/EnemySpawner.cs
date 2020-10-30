using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    private Transform _mobContainer;
    
    void Awake()
    {
        var mobContainerGameObject = GameObject.FindWithTag("MobContainer");

        if (_mobContainer == null)
        {
            Debug.LogWarning("MobContainer not found, instancing in root scene");
            _mobContainer = transform.root;
        }
        else
            _mobContainer = mobContainerGameObject.transform;
    }

    public GameObject SpawnObject(GameObject objectToSpawn)
    {
        var spawned = Instantiate(objectToSpawn);

        spawned.transform.parent = _mobContainer;
        spawned.GetComponent<MobAI>().SetTarget(spawned);
        
        return spawned;
    }
}
