// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Threading.Channels;
using FJ.BTC.APP.Models;

var random = new Random();
IBlock genesisBlock = new Block(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00});
byte[] difficultyMode = new byte[] {0x00, 0x00};

BlockChain chain = new BlockChain(difficultyMode, genesisBlock);
Console.WriteLine("BlockChain is starting validation");
var watch = Stopwatch.StartNew();
for (int i = 0; i < 100; i++)
{
    var data = Enumerable.Range(0, 255).Select(p => (byte) random.Next()).ToArray();
    chain.Add(new Block(data.ToArray()));
    Console.WriteLine(chain.LastOrDefault()?.ToString());
    if(await chain.IsValid())
        Console.WriteLine("BlockChain is Valid");
}
watch.Stop();
var elapsedMinute = watch.ElapsedMilliseconds / 1000;
Console.WriteLine("Elapsed time in seconds :" + elapsedMinute);
Console.ReadLine();