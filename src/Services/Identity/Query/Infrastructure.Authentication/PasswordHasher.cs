using Infrastructure.Authentication.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private const int iterCount = 10000;
    private readonly RandomNumberGenerator rng;

    public PasswordHasher()
    {
        rng = RandomNumberGenerator.Create();
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
        => VerifyHashedPassword(Convert.FromBase64String(hashedPassword), password);

    public string HashPassword(string password)
        => Convert.ToBase64String(HashPassword(password, rng));

    private static byte[] HashPassword(string password, RandomNumberGenerator rng)
        => HashPassword(
                password: password,
                rng: rng,
                prf: KeyDerivationPrf.HMACSHA512,
                iterCount: iterCount,
                saltSize: 128 / 8,
                numBytesRequested: 256 / 8);

    private static bool VerifyHashedPassword(byte[] hashedPassword, string password)
    {
        try
        {
            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            if (saltLength < 128 / 8)
                return false;

            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            int subkeyLength = hashedPassword.Length - 13 - salt.Length;

            if (subkeyLength < 128 / 8)
                return false;

            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
        catch
        {
            return false;
        }
    }

    private static byte[] HashPassword(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
    {
        byte[] salt = new byte[saltSize];

        rng.GetBytes(salt);

        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];

        outputBytes[0] = 0x01;

        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);

        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);

        return outputBytes;
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        => ((uint)buffer[offset + 0] << 24) |
            ((uint)buffer[offset + 1] << 16) |
            ((uint)buffer[offset + 2] << 8) |
            buffer[offset + 3];

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
}