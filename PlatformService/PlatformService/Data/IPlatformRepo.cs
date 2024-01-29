using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChange();

        IEnumerable<Platform> GetAll();
        Platform GetById(int id);
        void Create(Platform platform);
        //void Update(Platform platform);
        //void Delete(int id);
    }
}
