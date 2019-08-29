namespace p01_chatudp.Models
{
    public class State
    {
        public int BufferSize { get; }
        public State(int bufferSize)
        {
            BufferSize = bufferSize;
        }
        public byte[] GetBuffer()
        {
            return new byte[BufferSize];
        }
    }
}
