namespace Source.Scripts.Services.SavingService
{
    public interface ISavingService: IInitializationAwaiter
    {
        bool CheckSaves(string path);
        void Save(string path, ISaveContainer container);
        bool Load<T>(string path,T outContainer) where T : ISaveContainer;
    }
}
