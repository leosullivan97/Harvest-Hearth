using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D playerRB;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator playerAnim;

    public enum ToolType
    {
        plough,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

    public float toolWaitTime = 0.5f;
    private float toolWaitCounter;

    public Transform toolIndicator;
    public float toolRange = 3f;

    public CropController.CropType seedCropType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initializes UI for current tool and seed on game start
    private void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
        UIController.instance.SwitchSeed(seedCropType);
    }

    private void Update()
    {
        // Block movement if any UI overlay is active
        if (UIController.instance != null)
        {
            if (UIController.instance.theIC?.gameObject.activeSelf == true ||
                UIController.instance.theShop?.gameObject.activeSelf == true ||
                UIController.instance.pauseScreen?.gameObject.activeSelf == true)
            {
                playerRB.linearVelocity = Vector2.zero;
                return;
            }
        }

        // Lock movement while using a tool
        if (toolWaitCounter > 0)
        {
            toolWaitCounter -= Time.deltaTime;
            playerRB.linearVelocity = Vector2.zero;
        }
        else
        {
            playerRB.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

            if (playerRB.linearVelocity.x < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (playerRB.linearVelocity.x > 0f)
            {
                transform.localScale = Vector3.one;
            }
        }

        bool hasSwitchedTool = false;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;
            if ((int)currentTool >= 4)
            {
                currentTool = ToolType.plough;
            }
            hasSwitchedTool = true;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame) { currentTool = ToolType.plough; hasSwitchedTool = true; }
        if (Keyboard.current.digit2Key.wasPressedThisFrame) { currentTool = ToolType.wateringCan; hasSwitchedTool = true; }
        if (Keyboard.current.digit3Key.wasPressedThisFrame) { currentTool = ToolType.seeds; hasSwitchedTool = true; }
        if (Keyboard.current.digit4Key.wasPressedThisFrame) { currentTool = ToolType.basket; hasSwitchedTool = true; }

        if (hasSwitchedTool)
        {
            UIController.instance.SwitchTool((int)currentTool);
        }

        playerAnim.SetFloat("speed", playerRB.linearVelocity.magnitude);

        if (GridController.instance != null)
        {
            if (actionInput.action.WasPressedThisFrame())
            {
                UseTool();
            }

            // Update tool indicator position (clamped to tool range and grid)
            toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

            if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
            {
                Vector2 direction = (toolIndicator.position - transform.position).normalized * toolRange;
                toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
            }

            toolIndicator.position = new Vector3(
                Mathf.FloorToInt(toolIndicator.position.x) + 0.5f,
                Mathf.FloorToInt(toolIndicator.position.y) + 0.5f,
                0f
            );
        }
        else
        {
            toolIndicator.position = new Vector3(0f, 0f, -20f);
        }
    }

    // Applies the selected tool to the targeted block
    private void UseTool()
    {
        GrowBlock block = GridController.instance.GetBlock(
            toolIndicator.position.x - 0.5f,
            toolIndicator.position.y - 0.5f
        );

        toolWaitCounter = toolWaitTime;

        if (block != null)
        {
            switch (currentTool)
            {
                case ToolType.plough:
                    block.PloughSoil();
                    playerAnim.SetTrigger("usePlough");
                    break;

                case ToolType.wateringCan:
                    block.WaterSoil();
                    playerAnim.SetTrigger("useWateringCan");
                    break;

                case ToolType.seeds:
                    if (CropController.instance.GetCropInfo(seedCropType).seedAmount > 0)
                    {
                        block.PlantCrop(seedCropType);
                    }
                    break;

                case ToolType.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }

    // Updates the seed type currently selected for planting
    public void SwitchSeed(CropController.CropType newSeed)
    {
        seedCropType = newSeed;
    }
}
