using Teatro.Core.Scenes;
using Teatro.EntityFrameworkCore.Repositories.Base;

namespace Teatro.EntityFrameworkCore.Repositories;

public class SceneRepository : Repository<Scene, long>
{
    public SceneRepository(TeatroDbContext context) : base(context)
    {
    }
}