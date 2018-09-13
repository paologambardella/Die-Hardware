using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerJoystick : InputReceiverMonoBehaviour  
{
    [SerializeField] RectTransform joystickArea;
    [SerializeField] RectTransform[] toggles;

    [SerializeField] Image[] activatedImages;

    [SerializeField] float sensitivity = 2f;

    bool activated = false;
    float canvasAlpha = 0f;

    public void OnPointerDown()
    {
        SetActivatedAlpha(1f, 0.3f);

        UpdateTogglePosition();
        activated = true;

        GameController.instance.player.SetShooting(true);
    }

    public void OnPointerMove()
    {
        UpdateTogglePosition();
    }

    public void OnPointerUp()
    {
        SetActivatedAlpha(0f, 0.2f);
        activated = false;

        GameController.instance.player.SetShooting(false);
    }

    void Start()
    {
        SetActivatedAlpha(0f, 0f);
    }

    void SetActivatedAlpha(float alpha, float duration)
    {
        for (int i = 0; i < activatedImages.Length; ++i)
        {
            activatedImages[i].CrossFadeAlpha(alpha, duration, false);
        }
    }

    void UpdateTogglePosition()
    {
        float mousePosX01 = Input.mousePosition.x / Screen.width;
//        Debug.Log(Screen.width + " " + Input.mousePosition + " " + mousePosX01);

        for (int i = 0; i < toggles.Length; ++i)
        {
            Vector3 pos = toggles[i].position;
            pos.x = Input.mousePosition.x;
            toggles[i].position = pos;
        }

        SetPlayerPosition01(mousePosX01);
    }

    void SetPlayerPosition01(float x01)
    {
        float relativeX = Mathf.Clamp01(x01);
        relativeX -= 0.5f;
        relativeX *= 2f;
        relativeX *= sensitivity;

        if (!GameController.instance.IsGameStarted())
        {
            GameController.instance.StartGame();
        }

//        Debug.Log(relativeX);
        GameController.instance.player.SetHorizontalRelativePosition(Mathf.Clamp(relativeX * sensitivity, -1f, 1f));
    }

//    [SerializeField] Transform toggle;
//    [SerializeField] BoxCollider touchCollider;
//
//    Camera gameUICamera;
//    PlayerController player;
//
//    protected override void OnEnable()
//    {
//        base.OnEnable();
//
//        player = GameController.instance.player;
//        gameUICamera = GameController.instance.gameUICamera;
//    }
//
//    protected override void OnDisable()
//    {
//        base.OnDisable();
//    }
//
//    public override bool TouchBegan(Vector3 touchPosition)
//    {
//        Ray ray = GameController.instance.gameUICamera.ScreenPointToRay(touchPosition);
//        RaycastHit hit;
//
//        if (touchCollider.Raycast(ray, out hit, 30f))
//        {
//            player.SetShooting(true);
//
//            UpdateMove(touchPosition);
//
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//
//    public override bool TouchUpdated(Vector3 touchPosition)
//    {
//        UpdateMove(touchPosition);
//
//        return true;
//    }
//
//    public override bool TouchEnded(Vector3 touchPosition)
//    {
//        UpdateMove(touchPosition);
//
//        player.SetShooting(false);
//
//        return true;
//    }
//
//    void UpdateMove(Vector3 touchPosition)
//    {
//        //Update toggle position
//        Vector3 myPos = toggle.position;
//        Vector3 worldTouchPos = gameUICamera.ScreenToWorldPoint(touchPosition);
//        myPos.x = worldTouchPos.x;
//        toggle.position = myPos;
//
//        //Update player position
//        Vector3 viewportTouchPos = gameUICamera.ScreenToViewportPoint(touchPosition);
//        float joystick01 = Mathf.Clamp01(viewportTouchPos.x);
//
//        //map range between 0.2 and 0.8 to 0 and 1 to make it easier on the edges
//        joystick01 -= 0.2f;
//        joystick01 = Mathf.Clamp01(joystick01 / 0.6f);
//
//        player.SetHorizontalRelativePosition(joystick01);
//    }
}
