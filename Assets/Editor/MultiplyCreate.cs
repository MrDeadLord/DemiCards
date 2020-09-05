///Made by https://github.com/MrDeadLord
///Any questions/suggestions: https://stackoverflow.com/users/13863823/dead-lord
///or here: https://www.facebook.com/Mr.D.Lord
///
///Feel free to use. Hope you'll enjoy it ^_^
///
///Here you van make fine randon rocks placing, bot spawnpoints or anything you like
///
///P.S. soon I'll add eng coments. sor :(
///

using UnityEditor;
using UnityEngine;

public class MultiplyCreate : EditorWindow
{
    #region Переменные
    /// <summary>
    /// Умножаемый объект
    /// </summary>
    private GameObject gameObj;
    /// <summary>
    /// Кол-во умножаемых объектов
    /// </summary>
    private int count;
    Vector3 startPosition;
    float spaceBetween;
    string[] directions = { "X", "-X", "Y", "-Y", "Z", "-Z" };
    int directionIndex;
    private Vector3 spawnPosition;
    private Quaternion startRotation;

    //Доп настройки
    private bool isAdvanced = false;
    private string objName = "multiplied";  //Стандартное имя
    bool isGrouped;
    string parentsName = "Parrent"; //Стандартное имя родителя
    
    /// <summary>
    /// На сколько будет поворачиватся каждый последующий объект
    /// </summary>
    private Quaternion nextRotation;

    private Vector3 startRot, nextRot;  //Временные переменные
    #endregion

    [MenuItem("DeadLords/Create Multiply Objects %#d", false, 1)]
    public static void MultiplyWindow()
    {
        EditorWindow.GetWindow(typeof(MultiplyCreate));
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки создания объектов", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (Selection.activeGameObject)
        {
            gameObj = Selection.activeGameObject;   //Выделенный объект попадает в поле объекта
            startPosition = gameObj.transform.position; //Позиция объекта записывается в поле позиции старта

            startRot = gameObj.transform.rotation.eulerAngles;
            startRotation = gameObj.transform.rotation; //Поворот записывается в поле стартового поворота
        }


        gameObj = EditorGUILayout.ObjectField("Объект умножения", gameObj, typeof(GameObject), true) as GameObject;

        count = EditorGUILayout.IntSlider("Кол-во объектов", count, 1, 100);

        spaceBetween = EditorGUILayout.Slider("Расстояние между объектами", spaceBetween, 1, 50);

        directionIndex = EditorGUILayout.Popup("Направление умножения", directionIndex, directions);

        GUILayout.Space(5);
        startPosition = EditorGUILayout.Vector3Field("Расположение первого объекта", startPosition);

        startRot = EditorGUILayout.Vector3Field("Поворот первого объекта", startRot);
        startRotation = Quaternion.Euler(startRot);

        GUILayout.Space(5);
        nextRot = EditorGUILayout.Vector3Field("Поворот следующих объектов", nextRot);

        #region Доп. параметры
        GUILayout.Space(10);
        isAdvanced = EditorGUILayout.BeginToggleGroup("Доп. параметры", isAdvanced);

        objName = EditorGUILayout.TextField("Имя объектов", objName);

        GUILayout.Space(5);
        isGrouped = EditorGUILayout.BeginToggleGroup("Группировать под один объект", isGrouped);
        parentsName = EditorGUILayout.TextField("Имя родительского объекта", parentsName);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndToggleGroup();
        #endregion

        if (GUILayout.Button("Создать объекты"))
        {
            if (isGrouped)
            {
                var parent = new GameObject(parentsName);
                parent.transform.position = startPosition;
                parent.transform.rotation = startRotation;

                CreateObject(parent.transform);
            }
            else
                CreateObject(null);
        }
    }

    /// <summary>
    /// Создание объектов в зависимости от выбранных параметров
    /// </summary>
    /// <param name="parent">Объект под который нужно объеденить. Если не нужно - null</param>
    private void CreateObject(Transform parent)
    {
        switch (directionIndex)
        {
            //+x Ибо по умолчанию это именно +x
            default:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.x += spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-x
            case 1:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.x -= spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //+y
            case 2:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.y += spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-y
            case 3:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.y -= spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //+z
            case 4:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.z += spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-z
            case 5:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.z -= spaceBetween;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
        }        
    }
}
