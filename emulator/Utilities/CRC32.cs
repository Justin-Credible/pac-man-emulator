using System;
using System.Collections.Generic;
using System.Linq;

namespace JustinCredible.PacEmu
{
    /// <summary>
    /// Performs 32-bit reversed cyclic redundancy checks.
    /// Taken from: https://stackoverflow.com/a/51922169
    /// </summary>
    public class CRC32
    {
        #region Constants
        /// <summary>
        /// Generator polynomial (modulo 2) for the reversed CRC32 algorithm. 
        /// </summary>
        private const UInt32 s_generator = 0xEDB88320;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the Crc32 class.
        /// </summary>
        public CRC32()
        {
            // Constructs the checksum lookup table. Used to optimize the checksum.
            m_checksumTable = Enumerable.Range(0, 256).Select(i =>
            {
                var tableEntry = (uint)i;
                for (var j = 0; j < 8; ++j)
                {
                    tableEntry = ((tableEntry & 1) != 0)
                        ? (s_generator ^ (tableEntry >> 1)) 
                        : (tableEntry >> 1);
                }
                return tableEntry;
            }).ToArray();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the checksum of the byte stream.
        /// </summary>
        /// <param name="byteStream">The byte stream to calculate the checksum for.</param>
        /// <returns>A 32-bit reversed checksum.</returns>
        public UInt32 Get<T>(IEnumerable<T> byteStream)
        {
            try
            {
                // Initialize checksumRegister to 0xFFFFFFFF and calculate the checksum.
                return ~byteStream.Aggregate(0xFFFFFFFF, (checksumRegister, currentByte) => 
                        (m_checksumTable[(checksumRegister & 0xFF) ^ Convert.ToByte(currentByte)] ^ (checksumRegister >> 8)));
            }
            catch (FormatException e)
            {
                throw new Exception("CRC32: Could not read the stream out as bytes.", e);
            }
            catch (InvalidCastException e)
            {
                throw new Exception("CRC32: Could not read the stream out as bytes.", e);
            }
            catch (OverflowException e)
            {
                throw new Exception("CRC32: Could not read the stream out as bytes.", e);
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Contains a cache of calculated checksum chunks.
        /// </summary>
        private readonly UInt32[] m_checksumTable;

        #endregion
    }
}
