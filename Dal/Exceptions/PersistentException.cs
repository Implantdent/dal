namespace Dal.Exceptions
{
    /// <summary>
    /// Excepción personalizada para la persistencia de entidades
    /// </summary>
    [Serializable]
    public class PersistentException : Exception
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PersistentException() { }

        /// <summary>
        /// Crea una excepción con un mensaje
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        public PersistentException(string message) : base(message) { }

        /// <summary>
        /// Crea una excepción con un mensaje y una excepción interna
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="inner">Excepción interna</param>
        public PersistentException(string message, Exception inner) : base(message, inner) { }
    }
}
