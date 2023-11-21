using PRY.DataAcces.Bases;

namespace PRY.DataAcces.Interfaces
{
    public interface IBaseSevice<T>
    {
        public Task<BaseResponse<IEnumerable<T>>> Listar();
        public Task<BaseResponse<T>> GetByID(int id);
        public Task<BaseResponse<int>> Save(T usuario);
        public Task<BaseResponse<int>> Edit(T usuario);
        public Task<BaseResponse<bool>> Delete(int id);
    }
}

