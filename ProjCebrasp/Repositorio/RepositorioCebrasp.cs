using System.Data.SqlClient;
using System.Data;
using ProjCebrasp.Models;

namespace ProjCebrasp.Repositorio
{
    public class RepositorioCebrasp
    {

        private readonly string _connectionString;

        public RepositorioCebrasp(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InserirUsuario(UsuarioModel pessoa)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AdicionarPessoa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CPF", pessoa.Cpf);
                    command.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@Email", pessoa.Email);
                    command.Parameters.AddWithValue("@Identidade", pessoa.Identidade);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<UsuarioModel> ListarUsuario()
        {
            List<UsuarioModel> pessoas = new List<UsuarioModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ListarPessoas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsuarioModel usuario = new UsuarioModel();
                            usuario.Cpf = reader["Cpf"].ToString();
                            usuario.Nome = reader["Nome"].ToString();
                            usuario.Email = reader["Email"].ToString();
                            usuario.Identidade = reader["Identidade"].ToString();

                            pessoas.Add(usuario);
                        }
                    }
                }
            }

            return pessoas;
        }




    }
}
