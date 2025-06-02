namespace maisAgua.Domain.Exceptions
{
    internal class UniqueConstraintException : DomainException
    {
        public UniqueConstraintException(string atributo, Exception? ex) 
            : base($"ERRO. Violação de atributo único: {atributo}") { }
    }
}
