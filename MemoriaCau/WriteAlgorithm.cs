namespace MemoriaCau
{
    public enum WriteAlgorithms
    {
        CopyBack,
        WriteThroughWithAssignementOnWrite,
        WriteThroughWithoutAssignementOnWrite

    }
    public struct WriteAlgorithm
    {
        public readonly bool ShouldBringBlockOnWriteFail;
        public readonly WriteAlgorithms Type;
        public WriteAlgorithm(WriteAlgorithms type)
        {
            this.Type = type;
            switch (type)
            {
                case WriteAlgorithms.WriteThroughWithAssignementOnWrite:
                case WriteAlgorithms.CopyBack:
                    ShouldBringBlockOnWriteFail = true;
                    break;
                case WriteAlgorithms.WriteThroughWithoutAssignementOnWrite:
                    ShouldBringBlockOnWriteFail = false;
                    break;
                default:
                    ShouldBringBlockOnWriteFail = true;
                    break;
            }
        }
    }
}