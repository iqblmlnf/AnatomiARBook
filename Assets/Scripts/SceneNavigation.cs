using UnityEngine;
using UnityEngine.SceneManagement; // Library wajib untuk perpindahan halaman di Unity

public class SceneNavigation : MonoBehaviour
{
    // Fungsi untuk membuka halaman Scan AR
    public void LoadARScene()
    {
        SceneManager.LoadScene("Scene_ARScan");
    }

    // Fungsi untuk membuka halaman Menu Utama
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }

    // Fungsi untuk keluar dari aplikasi (opsional)
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Aplikasi ditutup");
    }
}