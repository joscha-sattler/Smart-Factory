using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory design pattern with generic twist!
/// </summary>
public abstract class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    // Reference to prefabs of whatever type.
    [SerializeField]
    protected List<T> prefabs = new List<T>();

    protected GameObject dynamicParent;

    // Start is called before the first frame update
    void Start()
    {
        dynamicParent = GameObject.Find("_Dynamic");
    }

    /// <summary>
    /// Creating new instance of prefab.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public T GetNewInstance(int index)
    {
        T newInstance = Instantiate(prefabs[index]);
        dynamicParent = GameObject.Find("_Dynamic");
        newInstance.transform.SetParent(dynamicParent.transform);

        return newInstance;
    }
}