namespace SistemaMatriculas.Application.Services
{
    public interface IService<T>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(T dto);
        Task<T> Update(int id, T dto);
        Task<T> Delete(int id);
        bool Validate(T dto, bool isUpdate);
    }
}
