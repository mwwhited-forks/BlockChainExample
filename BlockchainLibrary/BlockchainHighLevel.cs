using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockchainLibrary;

public class BlockchainHighLevel
{
    public LinkedList<Block> Blockchain { get; } = new();

    public BlockchainHighLevel() =>
        Blockchain.AddLast(BlockchainLowLevel.CreateGenesisBlock());

    public void AddBlock(string DataToAdd) =>
        Blockchain.AddLast(new Block(BlockchainLowLevel.HashBlock(Blockchain.Last.Value.BlockHash, DataToAdd, 0), Blockchain.Last.Value.BlockHash, DataToAdd));

    /// <summary>
    /// Find block by string hash
    /// 
    /// Search Through The Entire Block
    /// If we don't find anything, return Null.
    /// </summary>
    /// <param name="HashR"></param>
    /// <returns></returns>
    public Block FindBlock(string HashR) =>
        Blockchain.FirstOrDefault(item => item.HashR.Equals(HashR));

    /// <summary>
    /// Find block by binary hash
    /// 
    ///  Search Through The Entire Block
    /// If we don't find anything, return Null. 
    /// </summary>
    /// <param name="HashR"></param>
    /// <returns></returns>
    public Block FindBlock(byte[] Hash) =>
        Blockchain.FirstOrDefault(item => item.BlockHash.SequenceEqual(Hash));

    /// <summary>
    /// Verifys a single block
    /// </summary>
    /// <param name="BlockUnderTest"></param>
    /// <param name="PreviousBlock"></param>
    /// <param name="NextBlock"></param>
    /// <returns></returns>
    public bool VerifyBlock(Block BlockUnderTest, Block PreviousBlock, Block NextBlock) =>
        BlockchainLowLevel.VerifyBlock(BlockUnderTest, PreviousBlock, NextBlock);

    /// <summary>
    /// This is just for the video demo and acts as a pass-though to Low-level
    /// </summary>
    /// <param name="PreviousBlockHash"></param>
    /// <param name="TransactionData"></param>
    /// <param name="Nonce"></param>
    /// <returns></returns>
    public byte[] CreateASimpleBlockHash(byte[] PreviousBlockHash, string TransactionData, int Nonce) =>
        BlockchainLowLevel.HashBlock(PreviousBlockHash, TransactionData, Nonce);

}
