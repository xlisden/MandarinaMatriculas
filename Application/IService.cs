namespace SistemaMatriculas.Application
{
    public interface IService<T>
    {
        public List<string> Errors { get; }
        Task<T> GetById(int id);
        Task<T> Add(T InsertDTO);
        Task<T> Update(int id, T dto);
        Task<T> Delete(int id);
        bool Validate(T dto);
    }
}
