using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    public Transform minPoint, maxPoint;
    public GrowBlock baseGridBlock;
    public LayerMask gridBlockers;

    private Vector2Int gridSize;
    public List<BlockRow> blockRows = new List<BlockRow>();

    private void Awake()
    {
        instance = this;
    }

    // Generates the grid layout and populates it with GrowBlock tiles
    private void Start()
    {
        GenerateGrid();
    }

    // Instantiates GrowBlocks in a grid pattern and loads stored block data if available
    private void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startPoint = minPoint.position + new Vector3(0.5f, 0.5f, 0f);

        gridSize = new Vector2Int(
            Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
            Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y)
        );

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow());

            for (int x = 0; x < gridSize.x; x++)
            {
                GrowBlock newBlock = Instantiate(baseGridBlock, startPoint + new Vector3(x, y, 0f), Quaternion.identity);
                newBlock.transform.SetParent(transform);
                newBlock.soilSR.sprite = null;

                newBlock.SetGridPosition(x, y);
                blockRows[y].blocks.Add(newBlock);

                if (Physics2D.OverlapBox(newBlock.transform.position, new Vector2(0.9f, 0.9f), 0f, gridBlockers))
                {
                    newBlock.soilSR.sprite = null;
                    newBlock.preventUse = true;
                }

                if (GridInfo.instance.hasGrid)
                {
                    BlockInfo storedBlock = GridInfo.instance.theGrid[y].blocks[x];

                    newBlock.currentStage = storedBlock.currentStage;
                    newBlock.isWatered = storedBlock.isWatered;
                    newBlock.cropType = storedBlock.cropType;
                    newBlock.growFailChance = storedBlock.growFailChance;

                    newBlock.SetSoilSprite();
                    newBlock.UpdateCropSprite();
                }
            }
        }

        if (!GridInfo.instance.hasGrid)
        {
            GridInfo.instance.CreateGrid();
        }

        baseGridBlock.gameObject.SetActive(false);
    }

    // Returns the GrowBlock at a given world position, adjusted to grid coordinates
    public GrowBlock GetBlock(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        x -= minPoint.position.x;
        y -= minPoint.position.y;

        int intX = Mathf.RoundToInt(x);
        int intY = Mathf.RoundToInt(y);

        if (intX < gridSize.x && intY < gridSize.y)
        {
            return blockRows[intY].blocks[intX];
        }

        return null;
    }
}

[System.Serializable]
public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>();
}
