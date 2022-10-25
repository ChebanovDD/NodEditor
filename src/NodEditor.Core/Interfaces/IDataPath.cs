namespace NodEditor.Core.Interfaces
{
    public interface IDataPath
    {
        void Construct();
        bool Execute();
        void Reset();
    }
}