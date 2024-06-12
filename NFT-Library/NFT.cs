using System;

namespace NFT_Library;

public record NFT
{
    public string TransactionString { get; init; }

    public NFT(string from, string action, string to, byte[] nftImage) =>
        TransactionString = string.Join(Environment.NewLine, from, action, to, BitConverter.ToString(nftImage));
}



