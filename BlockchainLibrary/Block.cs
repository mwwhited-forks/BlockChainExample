using System;

namespace BlockchainLibrary;

public record Block
{
    public byte[] BlockHash { get; init; }
    public string HashR => BitConverter.ToString(BlockHash);
    public byte[] PreviousBlockHash { get; init; }
    public string PreviousHashR => BitConverter.ToString(PreviousBlockHash);
    public string Data { get; init; }

    /// <summary>
    /// Only used in the advanced example.
    /// </summary>
    public int Nonce { get; init; }

    public Block(byte[] blockHash, byte[] previousBlockHash, string data, int nonce = 0)
    {
        BlockHash = blockHash;
        PreviousBlockHash = previousBlockHash;
        Data = data;
        Nonce = nonce;
    }
}
