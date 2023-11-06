using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class for making singletons
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    //make instance property
    static T _instance;
    public static T _Instance
    {
        get
        {
            //null check
            if (_instance == null)
            {
                Debug.LogError(typeof(T).ToString() + " is missing.");
            }
            return _instance; 
        }
    }
    //set this to be the instance
    private void Awake()
    {
        //check if already exists. if so, destroy this as original is the only instance that should exist.
        if(_instance != null) 
        { 
            Debug.LogError(typeof(T).ToString() + "already has an instance. Destroying this object");
            Destroy(gameObject);
        }
        _instance = this as T;
        Init();
    }
    //helper virtual method for changing/rewriting this implementation
    public virtual void Init()
    {

    }
    //clean up on destruction
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
