using Cysharp.Threading.Tasks;

namespace Source.Scripts.Services.SceneService
{
    public interface ISceneService
    {
        public UniTask ChangeScene(SceneType type);
    }
}
