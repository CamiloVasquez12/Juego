using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void IrMenu()
   {
        SceneManager.LoadScene(0);
   }
   public void IrLogin()
   {
        SceneManager.LoadScene(1);
   }
   public void IrPlay()
   {
        SceneManager.LoadScene(3);
   }
   public void IrCredits(string credits)
   {
        SceneManager.LoadScene("credits");
   }
   public void Salir()
   {
        Application.Quit();
   }
}
