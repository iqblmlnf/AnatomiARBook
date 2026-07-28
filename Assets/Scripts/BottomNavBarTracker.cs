using UnityEngine;

public class BottomNavBarTracker : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("[BottomNavBarTracker] GameObject diaktifkan (OnEnable)!");
        // Cetak call stack lengkap untuk melacak siapa yang memanggil SetActive(true)
        Debug.Log("Call Stack OnEnable:\n" + System.Environment.StackTrace);
    }

    private void OnDisable()
    {
        Debug.Log("[BottomNavBarTracker] GameObject dinonaktifkan (OnDisable)!");
        Debug.Log("Call Stack OnDisable:\n" + System.Environment.StackTrace);
    }
}
