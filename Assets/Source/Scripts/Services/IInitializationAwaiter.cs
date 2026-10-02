using Cysharp.Threading.Tasks;

namespace Source.Scripts.Services
{
    public interface IInitializationAwaiter
    {
        public UniTaskCompletionSource Initialized {get; }
    }
}
