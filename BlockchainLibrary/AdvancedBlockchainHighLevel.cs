using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockchainLibrary;

public class AdvancedBlockchainHighLevel
{
    private readonly LinkedList<Block> _slockChain = new();
    private readonly List<string> _transactions = [];
    private readonly string _minerName;
    private static int _difficulty;

    private const int transactionLimit = 5; //<-- Change this to increase transaction limit
    private const int reward = 6; ///<-- Change this to increase reward

    public LinkedList<Block> Blockchain => _slockChain;

    public AdvancedBlockchainHighLevel(string MinerName, int Difficulty = 1)
    {
        _minerName = MinerName;
        _difficulty = Difficulty;
        _slockChain.AddLast(BlockchainLowLevel.CreateGenesisBlock());
    }

    public void Add(string Transaction)
    {
        _transactions.Add(Transaction);
        if (_transactions.Count == transactionLimit - 1)
        {

            //Add the reward transaction as the last transaction
            _transactions.Add(_minerName + " Rewards " + _minerName + " " + reward.ToString() + "BTC");

            //Set up the values to send to FindHash
            //Flatten the transactions
            string PreHashedTransactions = string.Join(Environment.NewLine, _transactions);
            Block PreviousBlock = _slockChain.Last.Value;

            _slockChain.AddLast(FindHashAndReturnBlock(PreHashedTransactions, PreviousBlock));
            _transactions.Clear(); //Reset the transaction
        }
    }

    private Block FindHashAndReturnBlock(string Transactions, Block PreviousBlock)
    {
        int nonce = 0;
        bool hashFound = false;
        byte[] currentBlockHash = new byte[20];


        while (hashFound != true && nonce != int.MaxValue)
        {
            currentBlockHash = BlockchainLowLevel.HashBlock(PreviousBlock.BlockHash, Transactions, nonce);

            if (TestForZeros(currentBlockHash) == true)
            {
                Block FoundBlock = new(currentBlockHash, PreviousBlock.BlockHash, Transactions, nonce);
                return FoundBlock;
            }

            //Always increment the nonce;
            nonce++;
        }

        //Will have to come up with a more graceful way of returning a failure.
        return null;

    }

    /// <summary>
    /// Walks the hash and makes sure the hash has the appropriate zeros for the difficulty
    /// </summary>
    /// <param name="Hash"></param>
    /// <returns></returns>
    private bool TestForZeros(byte[] Hash)
    {
        byte Zero = 0;
        byte[] ZeroByteArray = new byte[_difficulty];
        Buffer.BlockCopy(Hash, 0, ZeroByteArray, 0, _difficulty);


        foreach (var item in ZeroByteArray)
        {
            if (item != Zero)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Find block by string hash
    /// 
    /// Search Through The Entire Block
    /// If we don't find anything, return Null. 
    /// </summary>
    /// <param name="HashR"></param>
    /// <returns></returns>
    public Block FindBlock(string HashR) =>
        Blockchain.FirstOrDefault(item => HashR.Equals(item.HashR));

    /// <summary>
    /// Find block by binary hash
    ///         
    /// Search Through The Entire Block
    /// If we don't find anything, return Null. 
    /// </summary>
    /// <param name="HashR"></param>
    /// <returns></returns>
    public Block FindBlock(byte[] Hash) =>
        Blockchain.FirstOrDefault(item=> item.BlockHash.SequenceEqual(Hash));

    /// <summary>
    /// This is different than the basic blockchain code
    /// Here we are verifying that the Previous block hash
    /// the nonce and the data all match what is expected
    /// </summary>
    /// <param name="BlockToVerify"></param>
    /// <returns></returns>
    public bool VerifyBlock(Block BlockUnderTest, Block PreviousBlock, Block NextBlock)
    {

        //Do a special accounting for the Genesis Block
        if (PreviousBlock == null)
        {
            var genesisBlock = BlockchainLowLevel.CreateGenesisBlock();
            return genesisBlock.BlockHash.SequenceEqual(BlockUnderTest.BlockHash);
        }

        byte[] previousHash;
        byte[] blockUnderTestHash;
        string transactions;
        //Do special Accounting for the last block

        if (NextBlock == null)
        {

            previousHash = PreviousBlock.BlockHash;
            blockUnderTestHash = BlockUnderTest.BlockHash;
            transactions = BlockUnderTest.Data;
            int Nonce = BlockUnderTest.Nonce;

            byte[] calculatedHash = BlockchainLowLevel.HashBlock(previousHash, transactions, Nonce);
            return calculatedHash.SequenceEqual(blockUnderTestHash);
        }

        //If we're in the middle of the block.

        previousHash = PreviousBlock.BlockHash;
        blockUnderTestHash = BlockUnderTest.BlockHash;
        transactions = BlockUnderTest.Data;

        //Hash the block under test
        var calculatedHashOfBlockUnderTest = BlockchainLowLevel.HashBlock(PreviousBlock.BlockHash, BlockUnderTest.Data, BlockUnderTest.Nonce);

        //Use the result to hash the next block
        var calculatedNextBlockHash = BlockchainLowLevel.HashBlock(calculatedHashOfBlockUnderTest, NextBlock.Data, NextBlock.Nonce);

        //if the calculated value is equal to the stored value, the block has not been tampered with.
        return calculatedNextBlockHash.SequenceEqual(NextBlock.BlockHash);
    }
}
