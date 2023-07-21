using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class BaseObjectPool<T> : MonoBehaviour where T : Component
{
    public static BaseObjectPool<T> Instance { get; private set; }


    
    [SerializeField] protected List<T> prefabs;
    [SerializeField] protected T currentPrefab;
    
    private readonly Queue<T> objectPool = new Queue<T>();
    private readonly List<T> createdInstances = new List<T>();
    
    public virtual void SetPrefabType(string Type)
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }

    public T GetObject()
    {
        if (objectPool.Count == 0)
        {
            AddObject();
        }

        T obj = objectPool.Dequeue();
        createdInstances.Add(obj);
        return obj;
    }

    private void AddObject()
    {
        var newObject = Instantiate(currentPrefab);
        newObject.gameObject.SetActive(false);
        objectPool.Enqueue(newObject);
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectPool.Enqueue(objectToReturn);

        
    }


    public void ClearPreviousPrefabs()
    {
        objectPool.Clear();

        foreach (var obj in createdInstances)
        {
            Destroy(obj.gameObject);
        }
        
        createdInstances.Clear();
    }





}
