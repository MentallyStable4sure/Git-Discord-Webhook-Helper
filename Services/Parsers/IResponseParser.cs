
namespace MentallyStable.GitHelper.Services.Parsers
{
    public interface IResponseParser<TResponse, TActionTypes>
    {
        /// <summary>
        /// Parses prefixes contained in response and returns contained prefixes
        /// </summary>
        /// <param name="response">Response to parse</param>
        /// <param name="prefixes">All prefixes to parse</param>
        /// <returns>Prefixes found in this response</returns>
        public string[] ParsePrefixes(TResponse response, string[] prefixes);

        /// <summary>
        /// Parses prefixes contained in response and returns contained prefixes
        /// </summary>
        /// <param name="response">Response to parse</param>
        /// <param name="types">All types to parse</param>
        /// <returns>Types found in this response</returns>
        public TActionTypes[] ParseTypes(TResponse response, TActionTypes[] types);
    }
}
