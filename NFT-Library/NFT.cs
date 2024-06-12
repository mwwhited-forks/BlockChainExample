using System;

namespace NFT_Library;

public record NFT
{
    public string TransactionString { get; init; }

    public NFT(string From, string Action, string To, byte[] NFTImage) =>
        TransactionString = string.Join(Environment.NewLine, From, Action, To, BitConverter.ToString(NFTImage));
}



