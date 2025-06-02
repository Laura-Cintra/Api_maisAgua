namespace maisAgua.Domain.Exceptions
{
    public class ReadingNotFoundException : Exception
    {
        public ReadingNotFoundException(int id)
            : base($"Leitura com ID {id} não encontrada.")
        {
        }
    }
}
