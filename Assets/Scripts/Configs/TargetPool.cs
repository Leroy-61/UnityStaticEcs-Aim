using System.Collections.Generic;
using UnityEngine;

public class TargetPool
{
    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();

    public GameObject Get(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out Queue<GameObject> queue))
        {
            if (queue.Count > 0)
            {
                GameObject instance = queue.Dequeue();
                instance.SetActive(true);
                Debug.Log($"POOL: повторное использование {instance.name}");
                return instance;
            }
        }
        Debug.Log($"POOL: создание нового {prefab.name}");
        return Object.Instantiate(prefab);
    }

    public void Release(GameObject prefab, GameObject instance)
    {
        instance.SetActive(false);

        if (!pools.TryGetValue(prefab, out Queue<GameObject> queue))
        {
            queue = new Queue<GameObject>();
            pools.Add(prefab, queue);
        }

        queue.Enqueue(instance);
        Debug.Log($"POOL: возврат {instance.name}");
    }
}