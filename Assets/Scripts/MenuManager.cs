using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadTælning()
    {
        SceneManager.LoadScene("Tælning");
    }

    public void LoadFormer()
    {
        SceneManager.LoadScene("FormerTælning");
    }

    public void LoadStørrelser()
    {
        SceneManager.LoadScene("Størrelser");
    }

    public void LoadPlusstykker()
    {
        SceneManager.LoadScene("Plusstykker");
    }
}