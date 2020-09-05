using UnityEngine;

/// <summary>
/// Вращение и перемещение объекта к которому присвоен. TraylRender курсора
/// </summary>
public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _touchTrail;

    private Camera _cam;
    private Gyroscope _gyro;
    private bool gyroSupp;
    private GameObject instance;


    private void Start()
    {
        _cam = Camera.main;

        WebCamTexture camTexture = new WebCamTexture();

        GetComponent<MeshRenderer>().material.mainTexture = camTexture;

        camTexture.Play();

        gyroSupp = SystemInfo.supportsGyroscope;

        if (!gyroSupp) return;

        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                instance = Instantiate(_touchTrail, Input.mousePosition, Quaternion.identity);  //Создание там объекта

            FallowFinger();
            MoveByFinger();
        }

        if (!gyroSupp) return;

        RotateByGyro();
    }

    /// <summary>
    /// Генерация _gameObj в месте, где указатель коснулся поверхности
    /// </summary>
    private void FallowFinger()
    {                
        if (Input.touches[0].phase == TouchPhase.Moved)
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                instance.transform.position = hit.point;
            }            
        }
    }

    private void MoveByFinger()
    {
        Vector2 delta = Input.GetTouch(0).deltaPosition;

        Vector3 _position = gameObject.transform.position;

        _position.x = Mathf.Clamp(delta.x, -1, 1);
        gameObject.transform.position = _position;
    }

    private void RotateByGyro()
    {
        //Настройка скорости поворота
        /*float rotX = Mathf.Clamp(-Input.gyro.rotationRateUnbiased.x, -0.5f, 0.5f);
        float rotY = Mathf.Clamp(-Input.gyro.rotationRateUnbiased.y, -0.5f, 0.5f);
        float rotZ = Mathf.Clamp(-Input.gyro.rotationRateUnbiased.z, -0.5f, 0.5f);*/

        float rotX = -Input.gyro.rotationRateUnbiased.x;
        float rotY = -Input.gyro.rotationRateUnbiased.y;
        float rotZ = -Input.gyro.rotationRateUnbiased.z;

        gameObject.transform.Rotate(rotX, rotY, rotZ);

        gameObject.transform.rotation.Normalize();
    }
}
