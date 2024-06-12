using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace BlockchainLibrary;

static internal class BlockchainLowLevel
{
    private const string GenesisBlockText = "GenesisBlock";

    public static Block CreateGenesisBlock()
    {
        var previousBlockHash = BitConverter.GetBytes(0);
        var blockhash = HashBlock(previousBlockHash, GenesisBlockText, 0);
        var genesisBlock = new Block(blockhash, previousBlockHash, GenesisBlockText, 0);
        return genesisBlock;
    }

    public static byte[] HashBlock(byte[] previousBlockHash, string blockData, int nonce) =>
        SHA1.HashData(Encoding.ASCII.GetBytes(BitConverter.ToString(previousBlockHash) + nonce.ToString() + blockData));

    public static bool VerifyBlock(Block blockUnderTest, Block previousBlock, Block nextBlock) =>
        (previousBlock, nextBlock) switch
        {
            //if the Previous is Null, we are at the genesis block, 
            //so create a new one and comapre
            (null, _) => CreateGenesisBlock().BlockHash.SequenceEqual(blockUnderTest.BlockHash),

            // if the nextblock is null, we need to make sure the previous block matches the block to verify
            (_, null) => HashBlock(previousBlock.BlockHash, blockUnderTest.Data, blockUnderTest.Nonce).SequenceEqual(blockUnderTest.BlockHash),

            //If we're in the middle of the block.
            //Hash the block under test
            //Use the result to hash the next block
            //if the calculated value is equal to the stored value, the block has not been tampered with.
            (_,_)=> HashBlock(
                HashBlock(previousBlock.BlockHash, blockUnderTest.Data, blockUnderTest.Nonce),
                nextBlock.Data, nextBlock.Nonce).SequenceEqual(nextBlock.BlockHash)
        };
}
