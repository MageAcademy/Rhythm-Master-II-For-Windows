using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadedResources : MonoBehaviour
{
    public static PreloadedResources Instance;

    public TextAsset[] musicInformations;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync("Select Music");
    }
}