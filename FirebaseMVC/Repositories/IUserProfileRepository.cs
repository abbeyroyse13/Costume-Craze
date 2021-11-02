using CostumeCraze.Models;

namespace CostumeCraze.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        public UserProfile GetByUserProfileId(string userProfileId);
    }
}