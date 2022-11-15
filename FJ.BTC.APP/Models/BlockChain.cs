using System.Collections;

namespace FJ.BTC.APP.Models;

public class BlockChain : IEnumerable<IBlock>
{
    private List<IBlock> blockChainItems = new List<IBlock>();
    public byte[] DifficultyMode { get; }

    public List<IBlock> BlockChainItems
    {
        get => blockChainItems;
        set => blockChainItems = value;
    }

    public int Count => BlockChainItems.Count;

    public IBlock this[int index]
    {
        get => BlockChainItems[index];
        set => BlockChainItems[index] = value;
    }
    public BlockChain(byte[] difficultyMode, IBlock genesisBlock)
    {
        DifficultyMode = difficultyMode;
        genesisBlock.Hash = genesisBlock.MineHash(difficultyMode).Result;
        BlockChainItems.Add(genesisBlock);
    }


    public async Task Add(IBlock block)
    {
        if (blockChainItems.LastOrDefault() != null)
        {
            block.PreviousHash = blockChainItems.LastOrDefault().Hash;
        }

        block.Hash = await block.MineHash(DifficultyMode);
        blockChainItems.Add(block);
    }
    
    public IEnumerator<IBlock> GetEnumerator()
    {
        return BlockChainItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return BlockChainItems.GetEnumerator();
    }
}