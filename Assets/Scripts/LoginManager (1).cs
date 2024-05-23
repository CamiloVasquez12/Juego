using System;
using System.Collections;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public static LoginManager instance;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI inicioIncorrecto;
    public int idUser;
    //public static IdGlobal instancia;
    private void Start()
    {
        
    }
    void Awake()
    {
        // Si no hay ninguna instancia de DatosJuego, asigna esta como la instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia de DatosJuego, destruye esta instancia
            Destroy(gameObject);
        }
    }
    private void Update()
    {

    }

    [Obsolete]
    public void LoginButton()
    {
        string userName = usernameField.text;
        string password = passwordField.text;
        StartCoroutine(Login(userName, password));
        StartCoroutine(GetIdByUsername(userName));

    }
    public void RegisterButton()
    {
        string url = "https://www.google.com/search?q=goggloe&rlz=1C1ONGR_esCO1090CO1090&oq=goggloe&gs_lcrp=EgZjaHJvbWUyBggAEEUYOTIVCAEQLhgKGIMBGMcBGLEDGNEDGIAEMg8IAhAAGAoYgwEYsQMYgAQyDwgDEAAYChiDARixAxiABDIPCAQQABgKGIMBGLEDGIAEMgwIBRAAGAoYsQMYgAQyDwgGEAAYChiDARixAxiABDIGCAcQBRhA0gEIMzQzOGowajeoAgCwAgA&sourceid=chrome&ie=UTF-8";
        OpenWebPage(url);
    }
    public void OpenWebPage(string url)
    {
        Application.OpenURL(url);
    }

    [Obsolete]
    IEnumerator Login(string username, string password)
    {
        string url = $"http://www.pomponet.somee.com/apiPeople/Login?userName={username}&password={password}";

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Usuario o contraseña incorrecta " + request.error);
                inicioIncorrecto.text = "Error en la solicitud: " + request.error;
            }
            else
            {
                // Verificar el código de estado HTTP para asegurarse de que la solicitud fue exitosa
                if (request.responseCode == 200) 
                {
                    string responseBody = request.downloadHandler.text;
                    Debug.Log(responseBody);
                    bool loginSuccessful = bool.Parse(responseBody);
                    Debug.Log(loginSuccessful);


                    if (loginSuccessful)
                    {
                        Debug.Log("Inicio de sesión correcto");
                        SceneManager.LoadScene(2);
                    }
                    else
                    {
                        Debug.Log("Inicio de sesión incorrecto");
                        inicioIncorrecto.text = "Lo sentimos, tu usuario o contraseña son incorrectos.";
                    }
                }
                else
                {
                    Debug.Log("Error en la solicitud: Código de estado HTTP " + request.responseCode);
                    inicioIncorrecto.text = "Error en la solicitud: Código de estado HTTP " + request.responseCode;
                }
            }
        }
    }

    [Obsolete]
    IEnumerator GetIdByUsername(string username)
    {
        string url = $"http://berriessystemmanagement.somee.com/api/Login/IdByUsername/{username}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("No encuentro id" + www.error);
            }
            else
            {
                string responseBody = www.downloadHandler.text;
                Debug.Log(responseBody);
                idUser = int.Parse(responseBody);

                Debug.Log("ID del usuario: " + idUser);
            }
        }
    }
}
