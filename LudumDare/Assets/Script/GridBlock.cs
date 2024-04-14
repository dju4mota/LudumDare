using UnityEngine;
using UnityEngine.UI;
using static BlockManager;


public class GridBlock : MonoBehaviour
{
    public static GridBlock Instance;

    [SerializeField] public BlockEnum selectedBlock;
    [SerializeField] public Image block;
    [SerializeField] public Image slide;
    [SerializeField] public Image bounce;


    private void Awake(){
        Instance = this;
    }
    private void Start()
    {
        block.enabled = true;
        bounce.enabled = false;
        slide.enabled = false;
        selectedBlock = BlockEnum.Block;
    }

    public BlockEnum changeBlock()
    {        
        switch (selectedBlock)
        {
            case BlockEnum.Block:
                block.enabled = false;
                bounce.enabled = true;
                slide.enabled = false;
                selectedBlock = BlockEnum.Bounce;
                return selectedBlock;

            case BlockEnum.Bounce:
                block.enabled = false;
                bounce.enabled = false;
                slide.enabled = true;
                selectedBlock = BlockEnum.Slide;
                return selectedBlock;

            case BlockEnum.Slide:
                block.enabled = true;
                bounce.enabled = false;
                slide.enabled = false;
                selectedBlock = BlockEnum.Block;
                return selectedBlock;
        }
        selectedBlock = BlockEnum.Bounce;
        return selectedBlock;
    }


}

