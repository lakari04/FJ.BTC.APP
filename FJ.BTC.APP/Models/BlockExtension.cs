using System.Security.Cryptography;
using System.Text;

namespace FJ.BTC.APP.Models;

public static class BlockExtension
{
    public static async Task<byte[]> GenerateHash(this IBlock block)
    {
        using (var sha512 = SHA512.Create())
        using (MemoryStream stream = new MemoryStream())
        using (BinaryWriter bWriter = new BinaryWriter(stream))
        {
            bWriter.Write(block.Data);
            bWriter.Write(block.Nonce);
            bWriter.Write(block.PreviousHash);
            bWriter.Write(block.TimeStamp.ToString());
            // var streamArray = stream.ToArray();
            return await sha512.ComputeHashAsync(stream);
        }
    }

    public static async Task<byte[]> MineHash(this IBlock block, byte[] difficultyMode)
    {
        if (difficultyMode == null) throw new ArgumentNullException(nameof(difficultyMode));
        byte[] hash = Array.Empty<byte>();
        while (!hash.Take(2).SequenceEqual(difficultyMode))
        {
            block.Nonce++;
            hash = await block.GenerateHash();
        }

        return hash;
    }

    public static async Task<bool> IsValid(this IBlock block)
    {
        var blockHash = await block.GenerateHash();
        return block.Hash.SequenceEqual(blockHash);
    }

    public static async Task<bool> IsPreviousBlockValid(this IBlock block, IBlock previousBlock)
    {
        if (previousBlock == null) throw new ArgumentNullException(nameof(previousBlock));
        return await previousBlock.IsValid() && block.PreviousHash.SequenceEqual(previousBlock.Hash);
    }

    public static async Task<bool> IsValid(this IEnumerable<IBlock> blockChain)
    {
        var enumerables = blockChain.ToList();
        for (int i = 1; i < enumerables.Count; i++)
        {
            if (await enumerables[i].IsValid())
            {
                if (await enumerables[i].IsPreviousBlockValid(enumerables[i]))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
        //
        // return enumerables.Zip(enumerables.Skip(1), Tuple.Create).All(block =>
        //     block.Item2.IsValid() && block.Item2.IsPreviousBlockValid(block.Item2));
    }
}