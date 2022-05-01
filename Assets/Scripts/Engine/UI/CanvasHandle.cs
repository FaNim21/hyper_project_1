using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CanvasHandle : MonoBehaviour
{
    [Header("Komponenty")]
    public Canvas canvas;
    public PlayerController playerController;
    public CameraController cameraController;

    [Header("Canvas objects")]
    public GameObject[] uiObjects;
    public GameObject canvasPhone;
    public GameObject canvasMainMenu;

    [Header("Debug")]
    [ReadOnly] public bool isCanvasEnabled;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvasPhone.activeSelf)
                canvasPhone.SetActive(true);
            else if (!canvasMainMenu.activeSelf)
                canvasPhone.SetActive(false);
            else
                ChangeVisibility(ref canvasMainMenu);
        }
    }

    public void ChangeUIObject(string name)
    {
        name = name.ToLower();
        for (int i = 0; i < uiObjects.Length; i++)
        {
            var currentObject = uiObjects[i];

            if (currentObject.name.ToLower().StartsWith(name))
            {
                currentObject.SetActive(true);
                ChangeTimeScale(true);
            }
            else if (currentObject.activeSelf) currentObject.SetActive(false);
        }
    }
    private void ChangeVisibility(ref GameObject gameObject, bool isGamePausing = true)
    {
        if (isCanvasEnabled && !gameObject.activeSelf) return;

        gameObject.SetActive(!gameObject.activeSelf);

        ChangeTimeScale(isGamePausing);
    }
    private void ChangeTimeScale(bool isGamePausing)
    {
        if (!isGamePausing) return;
        isCanvasEnabled = !isCanvasEnabled;
        Time.timeScale = isCanvasEnabled ? 0 : 1;
    }

    public void Resume()
    {
        ChangeVisibility(ref canvasMainMenu);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
