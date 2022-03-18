using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : GameplayObject
{
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                SendMessage(new GameEvents.MouseClick()
                {
                    WorldPosition = hitInfo.point
                });
            }
        }
        else if (Input.GetMouseButton(1))
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
    }

    #region Section6

    private GlobalEvents globalEventBus;

    protected override void OnAwake(IEventHandler eventHandler)
    {
        globalEventBus = new GlobalEvents(eventHandler);
    }

    private void OnDestroy()
    {
        globalEventBus.Dispose();
    }

    #endregion
}
