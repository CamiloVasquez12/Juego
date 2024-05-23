using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Post : MonoBehaviour
{
    public static Post Instance;
    private void Awake()
    {

        //Si hay m�s de una instancia, destruye la otra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Establece la estancia estatica
            Instance = this;
        }
    }

    [System.Obsolete]
    public void Start()
    {
        int newCropNumber = Random.Range(1, 10); 
        int newIdPlayer = Random.Range(1, 100);
        StartCoroutine(Upload(newCropNumber, newIdPlayer));
    }
    [System.Obsolete]
    IEnumerator Upload(int Crop_Number, int Id_Player)
    {
        string url = "http://www.pomponet.somee.com/apiCrops?Crop_Number=" + Crop_Number + "&Id_Player=" + Id_Player;
        using UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error " + request.error);
        }
        else
        {
            Debug.Log("Form upload complete!" + Crop_Number + Id_Player);
        }
    }
}
