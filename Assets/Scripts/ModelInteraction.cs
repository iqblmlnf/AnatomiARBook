using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotateSpeed = 0.5f;
    private Vector3 initialRotation;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.01f;
    [Tooltip("Batas perkecil (0.2 = 20% dari ukuran awal)")]
    public float minScale = 0.2f;
    [Tooltip("Batas perbesar (5.0 = 500% dari ukuran awal)")]
    public float maxScale = 5.0f;
    private Vector3 initialScale;
    private float currentScaleMultiplier = 1.0f;

    private void Start()
    {
        // Menyimpan rotasi dan skala awal untuk fitur reset
        initialRotation = transform.localEulerAngles;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // 1. Logika untuk Mobile Touch (Layar Sentuh HP)
        if (Input.touchCount > 0)
        {
            // Rotasi dengan 1 Jari
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float rotationX = touch.deltaPosition.x * rotateSpeed;
                    float rotationY = touch.deltaPosition.y * rotateSpeed;

                    // Memutar objek berdasarkan geseran jari
                    transform.Rotate(Vector3.up, -rotationX, Space.World);
                    transform.Rotate(Vector3.right, rotationY, Space.World);
                }

                // Reset Posisi dengan Double Tap (Ketuk 2 kali dengan cepat)
                if (touch.tapCount == 2)
                {
                    ResetModel();
                }
            }
            // Zoom dengan 2 Jari (Pinch In / Pinch Out)
            else if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // Modifikasi: Menggunakan multiplier skala relatif
                currentScaleMultiplier -= (deltaMagnitudeDiff * zoomSpeed);
                currentScaleMultiplier = Mathf.Clamp(currentScaleMultiplier, minScale, maxScale);

                transform.localScale = initialScale * currentScaleMultiplier;
            }
        }
        // 2. Logika untuk Mouse (Uji coba di Laptop / Unity Editor)
        else
        {
            // Rotasi dengan klik kiri mouse & digeser
            if (Input.GetMouseButton(0))
            {
                float rotationX = Input.GetAxis("Mouse X") * rotateSpeed * 15f;
                float rotationY = Input.GetAxis("Mouse Y") * rotateSpeed * 15f;

                transform.Rotate(Vector3.up, -rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);
            }

            // Zoom dengan Scroll Wheel Mouse
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                // Modifikasi: Menggunakan multiplier skala relatif
                currentScaleMultiplier += (scroll * 0.5f);
                currentScaleMultiplier = Mathf.Clamp(currentScaleMultiplier, minScale, maxScale);
                
                transform.localScale = initialScale * currentScaleMultiplier;
            }

            // Reset dengan Klik Kanan Mouse
            if (Input.GetMouseButtonDown(1))
            {
                ResetModel();
            }
        }
    }

    // Fungsi untuk mengembalikan objek ke ukuran dan rotasi semula
    public void ResetModel()
    {
        transform.localEulerAngles = initialRotation;
        transform.localScale = initialScale;
    }
}