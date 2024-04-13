using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //[SerializeField] private List<GameObject> blockList;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject slide;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject bounce;


    public enum BlockEnum
    {
        Block,
        Slide,
        Bounce,
        Timer

    }


    private void Start()
    {
        
    }


    public void addBlock(BlockEnum blockType, Vector3 pos)
    {
        switch (blockType)
        {
            case BlockEnum.Block:
                Instantiate(block, pos, Quaternion.identity);
                //GameObject obj = Instantiate(block, pos, Quaternion.identity);
                //blockList.Add(obj);
                break;
            case BlockEnum.Bounce:
                Instantiate(bounce, pos, Quaternion.identity);
                break;
            case BlockEnum.Slide:
                Instantiate(slide, pos, Quaternion.identity);
                break;
            case BlockEnum.Timer:
                Instantiate(timer, pos, Quaternion.identity);
                break;
        }
    }


    public void removeBlock()
    {

    }
}
