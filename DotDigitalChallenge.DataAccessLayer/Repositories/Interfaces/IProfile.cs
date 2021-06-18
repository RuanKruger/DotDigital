using DotDigitalChallenge.DataAccessLayer.Models;

namespace DotDigitalChallenge.DataAccessLayer.Repositories.Interfaces
{
    public interface IProfile
    {
        public Profile GetCurrentProfile();
    }
}