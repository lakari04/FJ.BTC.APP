using System.Security.Cryptography;
using System.Text;

namespace FJ.BTC.APP.Models;

public interface IBlock
{
    public byte[] Data { get; }
    public byte[] Hash { get; set; }
    public int Nonce { get; set; }
    public byte[] PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }
}

public class Block : IBlock
{
    public byte[] Data { get; }
    public byte[] Hash { get; set; }
    public int Nonce { get; set; }
    public byte[] PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }


    public Block(byte[] data)
    {
        Data = data ?? throw new ArgumentException(nameof(data));
        Nonce = 0;
        PreviousHash = new byte[] {0x00};
        TimeStamp = DateTime.Now;
    }

    public override string ToString()
    {
        return
            $"{BitConverter.ToString(Hash).Replace("-", "")} :/n {BitConverter.ToString(PreviousHash).Replace("-", "")} :/n {Nonce} {TimeStamp}";
    }
}



