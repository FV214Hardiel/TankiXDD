using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleCode : MonoBehaviour
{


    // called zero
    //string typec = "Health";
    void Awake()
    {
        //Debug.Log("Awake");
    }

    // called first
    void OnEnable()
    {
       // Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        
    }

    // called third
    void Start()
    {
              
        //Debug.Log(typey);
        //gameObject.AddComponent(System.Type.GetType(typec));
    }

    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}