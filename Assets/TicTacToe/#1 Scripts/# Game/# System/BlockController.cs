using System.Collections.Generic;
using UnityEngine;
using static Block;


public class BlockController : MonoBehaviour
{
    [SerializeField] private List<Block> blocks = new List<Block>();

    public delegate void OnBlockClicked(int index);

    public OnBlockClicked onBlockClicked;

    public void InitBlocks()
    {
        foreach (Transform child in transform)
        {
            Block block = child.GetComponent<Block>();
            if (block != null)
            {
                blocks.Add(block);
            }
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].InitMarker(i, blockIndex =>
            {
                onBlockClicked?.Invoke(blockIndex);
            });
        }
    }

    public void PlaceMarker(int blockIndex, Constants.PlayerType playerType)
    {
        switch (playerType)
        {
            case Constants.PlayerType.PlayerA:
                blocks[blockIndex].SetMarker(Block.MarkerType.O);
                break;
            case Constants.PlayerType.PlayerB:
                blocks[blockIndex].SetMarker(Block.MarkerType.X);
                break;
        }
    }
}
