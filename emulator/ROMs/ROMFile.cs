
namespace JustinCredible.PacEmu
{
    /**
     * Used to encapsulate information about a single ROM file.
     */
    public class ROMFile
    {
        public string FileName { get; set; }
        public string AlternateFileName { get; set; }
        public int Size { get; set; }
        public string CRC32 { get; set; }
        public string Description { get; set; }
    }
}
