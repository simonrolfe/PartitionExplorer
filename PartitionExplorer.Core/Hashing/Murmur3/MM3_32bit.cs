/*
This code is public domain.

The MurmurHash3 algorithm was created by Austin Appleby and put into the public domain.  See http://code.google.com/p/smhasher/

This C# variant was authored by
Elliott B. Edwards and was placed into the public domain as a gist
Status...Working on verification (Test Suite)
Set up to run as a LinqPad (linqpad.net) script (thus the ".Dump()" call)

Adapted to work on strings, made not static
*/
using PartitionExplorer.Core.Hashing;
using System.IO;
using System.Text;

public class MM3_32bit : IStringHasher<int>
{
    //Change to suit your needs
    private uint _seed = 144;

    public void SetSeed(uint seed)
    {
        _seed = seed;
    }

    public string Name { get { return "Murmur3 32 bit"; } }

    public int Hash(string input)
    {
        Encoding utf8Encoding = new UTF8Encoding();

        byte[] inputBytes = utf8Encoding.GetBytes(input);
        using (MemoryStream ms = new MemoryStream(inputBytes))
        {
            return Hash(ms);
        }
    }

    public int Hash(Stream stream)
    {
        const uint c1 = 0xcc9e2d51;
        const uint c2 = 0x1b873593;

        uint h1 = _seed;
        uint k1 = 0;
        uint streamLength = 0;

        using (BinaryReader reader = new BinaryReader(stream))
        {
            byte[] chunk = reader.ReadBytes(4);
            while (chunk.Length > 0)
            {
                streamLength += (uint)chunk.Length;
                switch (chunk.Length)
                {
                    case 4:
                        /* Get four bytes from the input into an uint */
                        k1 = (uint)
                           (chunk[0]
                          | chunk[1] << 8
                          | chunk[2] << 16
                          | chunk[3] << 24);

                        /* bitmagic hash */
                        k1 *= c1;
                        k1 = rotl32(k1, 15);
                        k1 *= c2;

                        h1 ^= k1;
                        h1 = rotl32(h1, 13);
                        h1 = h1 * 5 + 0xe6546b64;
                        break;
                    case 3:
                        k1 = (uint)
                           (chunk[0]
                          | chunk[1] << 8
                          | chunk[2] << 16);
                        k1 *= c1;
                        k1 = rotl32(k1, 15);
                        k1 *= c2;
                        h1 ^= k1;
                        break;
                    case 2:
                        k1 = (uint)
                           (chunk[0]
                          | chunk[1] << 8);
                        k1 *= c1;
                        k1 = rotl32(k1, 15);
                        k1 *= c2;
                        h1 ^= k1;
                        break;
                    case 1:
                        k1 = (uint)(chunk[0]);
                        k1 *= c1;
                        k1 = rotl32(k1, 15);
                        k1 *= c2;
                        h1 ^= k1;
                        break;

                }
                chunk = reader.ReadBytes(4);
            }
        }

        // finalization, magic chants to wrap it all up
        h1 ^= streamLength;
        h1 = fmix(h1);

        unchecked //ignore overflow
        {
            return (int)h1;
        }
    }

    private uint rotl32(uint x, byte r)
    {
        return (x << r) | (x >> (32 - r));
    }

    private uint fmix(uint h)
    {
        h ^= h >> 16;
        h *= 0x85ebca6b;
        h ^= h >> 13;
        h *= 0xc2b2ae35;
        h ^= h >> 16;
        return h;
    }
}