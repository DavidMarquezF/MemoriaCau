namespace MemoriaCau
{
    public struct ProblemOption
    {
        public WriteAlgorithms WriteAlgorithm { get; }
        public UbicationAlgorithm UbicationAlgorithm { get; }
        public int? Ways { get; }

        public ProblemOption(UbicationAlgorithm ubicationAlg, WriteAlgorithms writeAlgo)
        {
            WriteAlgorithm = writeAlgo;
            UbicationAlgorithm = ubicationAlg;
            Ways = null;
        }

        public ProblemOption(UbicationAlgorithm ubicationAlg, WriteAlgorithms writeAlg, int ways)
        {
            WriteAlgorithm = writeAlg;
            UbicationAlgorithm = ubicationAlg;
            Ways = ways;
        }
        public override string ToString()
        {
            var txt = "";
            switch (UbicationAlgorithm)
            {
                case UbicationAlgorithm.Direct:
                    txt += "Correspondència directa ";
                    break;
                case UbicationAlgorithm.Associative:
                    txt += "Correspondència associativa ";
                    break;
                case UbicationAlgorithm.SetAssociative:
                    txt += string.Format("Correspondència associativa per conjunts amb {0} vies ", Ways);
                    break;
            }
            switch (WriteAlgorithm)
            {
                case WriteAlgorithms.CopyBack:
                    txt += "amb post-escriptura";
                    break;
                case WriteAlgorithms.WriteThroughWithAssignementOnWrite:
                    txt += "i escriptura immediata amb assignació d'escriptura";
                    break;
                case WriteAlgorithms.WriteThroughWithoutAssignementOnWrite:
                    txt += "i escriptura immediata sense assignació d'escriptura";
                    break;
            }
            return txt;
        }
    }

    public static class ProblemOptions
    {

        private static readonly ProblemOption[] Options = new ProblemOption[11]{
            new ProblemOption(UbicationAlgorithm.Direct, WriteAlgorithms.WriteThroughWithAssignementOnWrite),
            new ProblemOption(UbicationAlgorithm.Direct, WriteAlgorithms.CopyBack),
            new ProblemOption(UbicationAlgorithm.Associative, WriteAlgorithms.WriteThroughWithAssignementOnWrite),
            new ProblemOption(UbicationAlgorithm.Associative, WriteAlgorithms.WriteThroughWithoutAssignementOnWrite),
            new ProblemOption(UbicationAlgorithm.Associative, WriteAlgorithms.CopyBack),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.WriteThroughWithAssignementOnWrite, 2),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.WriteThroughWithAssignementOnWrite, 4),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.WriteThroughWithoutAssignementOnWrite, 2),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.WriteThroughWithoutAssignementOnWrite, 4),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.CopyBack, 2),
            new ProblemOption(UbicationAlgorithm.SetAssociative, WriteAlgorithms.CopyBack, 4)
        };

        public static ProblemOption getOption(int index)
        {
            return Options[index];
        }
    }
}