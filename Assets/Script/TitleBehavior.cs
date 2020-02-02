using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehavior : MonoBehaviour
{
    public string sceneName;
    public string finishedName;

    private AssetBundle loadedAssetBundle;
    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        loadedAssetBundle = AssetBundle.LoadFromFile("Assets/scenes");
        renderer = GetComponent<MeshRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled && PlayerPrefs.GetInt(finishedName,0) == 1)
        {
            enabled = false;
        }
    }


    private void OnMouseOver()
    {
        renderer.material.color = new Color32(100, 100, 100, 128);
    }

    private void OnMouseExit()
    {
        renderer.material.color = new Color32(100, 100, 100, 200);
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

}
