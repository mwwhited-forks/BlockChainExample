using BlockchainLibrary;
using NFT_Library;
using System;

namespace BlockChainExample;

public static class BasicBlockchain
{
    public static void GenerateBasicBlockChain()
    {
        //Create a blockchain and add some transactions
        var be = new BlockchainHighLevel();
        be.AddBlock("Ryan Pays PoshRyan 10 BTC for lunch");
        be.AddBlock("Seymore pays 20 BTC to Moo2You for Milkshake.");

        int itemNumber = 0;
        foreach (var item in be.Blockchain)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Block Height: " + itemNumber);
            Console.WriteLine("Block Hash: " + item.HashR);
            Console.WriteLine("Previous Hash: " + item.PreviousHashR);
            Console.WriteLine("Transaction Data: " + item.Data);
            Console.WriteLine("---------------------------");
            Console.WriteLine("");
            itemNumber++;
        }
    }

    public static void GenerateBasicNFTFromCommandLine(string[] args)
    {
        var be = new BlockchainHighLevel();
        var nft = NFTHighLevel.MintNFT(from: args[0], action: args[1], to: args[2], filename: args[3]);
        be.AddBlock(nft);

        int itemNumber = 0;
        foreach (var item in be.Blockchain)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Hash: " + item.HashR);
            Console.WriteLine("Previous Hash: " + item.PreviousHashR);
            Console.WriteLine("Data: " + item.Data);
            Console.WriteLine("---------------------------");
            Console.WriteLine("");
            itemNumber++;
        }
    }
}
