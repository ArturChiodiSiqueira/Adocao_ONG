using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adocao_ONG
{
    internal class Banco
    {
        string Conexao = "Data Source=localhost;Initial Catalog=Adocao_ONG;User id=sa;Password=cear2712;";

        public Banco()
        {
        }

        public string Caminho()
        {
            return Conexao;
        }
        
        #region CRUD PESSOA
        public void InserirPessoaBanco()
        {
            Pessoa pessoa = new Pessoa();

            pessoa.CadastraPessoa();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Pessoa(Cpf, Nome_Pes, Sexo, Data_Nascimento, Telefone, Logradouro, Numero, Bairro, Cidade, Estado, Cep) VALUES (@Cpf, @Nome_Pes, @Sexo, @Data_Nascimento, @Telefone, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @Cep);";

            SqlParameter cpf = new SqlParameter("@Cpf", SqlDbType.VarChar, 11);
            SqlParameter nomePes = new SqlParameter("@Nome_Pes", SqlDbType.VarChar, 50);
            SqlParameter sexo = new SqlParameter("@Sexo", SqlDbType.Char, 1);
            SqlParameter dataNascimento = new SqlParameter("@Data_Nascimento", SqlDbType.VarChar, 8);
            SqlParameter telefone = new SqlParameter("@Telefone", SqlDbType.VarChar, 14);
            SqlParameter logradouro = new SqlParameter("@Logradouro", SqlDbType.VarChar, 50);
            SqlParameter numero = new SqlParameter("@Numero", SqlDbType.Int);
            SqlParameter bairro = new SqlParameter("@Bairro", SqlDbType.VarChar, 50);
            SqlParameter cidade = new SqlParameter("@Cidade", SqlDbType.VarChar, 50);
            SqlParameter estado = new SqlParameter("@Estado", SqlDbType.VarChar, 50);
            SqlParameter cep = new SqlParameter("@Cep", SqlDbType.VarChar, 50);

            cpf.Value = pessoa.Cpf;
            nomePes.Value = pessoa.Nome;
            sexo.Value = pessoa.Sexo;
            dataNascimento.Value = pessoa.DataNascimento;
            telefone.Value = pessoa.TelefoneContato;
            logradouro.Value = pessoa.Logradouro;
            numero.Value = pessoa.Numero;
            bairro.Value = pessoa.Bairro;
            cidade.Value = pessoa.Cidade;
            estado.Value = pessoa.Estado;
            cep.Value = pessoa.Cep;

            cmd.Parameters.Add(cpf);
            cmd.Parameters.Add(nomePes);
            cmd.Parameters.Add(sexo);
            cmd.Parameters.Add(dataNascimento);
            cmd.Parameters.Add(telefone);
            cmd.Parameters.Add(logradouro);
            cmd.Parameters.Add(numero);
            cmd.Parameters.Add(bairro);
            cmd.Parameters.Add(cidade);
            cmd.Parameters.Add(estado);
            cmd.Parameters.Add(cep);

            cmd.Connection = conexaosql;
            cmd.ExecuteNonQuery();

            conexaosql.Close();

            Console.WriteLine("Cadastro Concluído\n");
            Console.ReadKey();
        }

        public void SelecionarPessoasBanco()
        {
            Pessoa pessoa = new Pessoa();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Cpf, Nome_Pes, Sexo, Data_Nascimento, Telefone, Logradouro, Numero, Bairro, Cidade, Estado, Cep FROM Pessoa";

            cmd.Connection = conexaosql;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}", reader.GetString(0));
                    Console.WriteLine("{0}", reader.GetString(1));
                    Console.WriteLine("{0}", reader.GetString(2));
                    Console.WriteLine("{0}", reader.GetString(3));
                    Console.WriteLine("{0}", reader.GetString(4));
                    Console.WriteLine("{0}", reader.GetString(5));
                    Console.WriteLine("{0}", reader.GetInt32(6));
                    Console.WriteLine("{0}", reader.GetString(7));
                    Console.WriteLine("{0}", reader.GetString(8));
                    Console.WriteLine("{0}", reader.GetString(9));
                    Console.WriteLine("{0}", reader.GetString(10));
                    Console.WriteLine("\n----------------------------\n");
                }
            }
            conexaosql.Close();
            Console.ReadKey();
        }

        public void DeletarPessoaBanco()
        {
            Pessoa pessoa = new Pessoa();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM Pessoa where Cpf = @Cpf";
            cmd.Connection = conexaosql;

            Console.Write("Informe o CPF da pessoa que deseja deletar: ");
            string cpfRemovido = Console.ReadLine();

            cmd.Parameters.Add(new SqlParameter("@Cpf", cpfRemovido));

            cmd.ExecuteNonQuery();

            Console.WriteLine("Pessoa deletada com sucesso.");

            conexaosql.Close();
        }

        public void AtualizarPessoaBanco()
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand();
            Banco conn = new Banco();
            SqlConnection conexaoSql = new SqlConnection(conn.Caminho());

            Console.Write("Informe o CPF que deseja atualizar");
            string cpfDigitado = Console.ReadLine();

            conexaoSql.Open();

            cmd = new();

            cmd.CommandText = "SELECT * FROM Pessoa where Cpf = @Cpf";

            cmd.Connection = conexaoSql;

            cmd.Parameters.Add(new SqlParameter("@Cpf", cpfDigitado));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("CPF digitado não localizado!");
                Console.ReadKey();
                return;
            }

            int opcao = 0;
            do
            {
                Console.WriteLine("Informe a opcao que deseja alterar: ");
                Console.WriteLine(" 1 - Nome");
                Console.WriteLine(" 2 - Sexo");
                Console.WriteLine(" 3 - Data de Nascimento");
                Console.WriteLine(" 4 - Telefone");
                Console.WriteLine(" 5 - Endereço");
                Console.Write(" Informe a opcao: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

            } while (opcao < 1 || opcao > 5);

            switch (opcao)
            {
                case 1:
                    Console.Write("Informe o novo nome: ");
                    string novoNome = Console.ReadLine().ToUpper();

                    cmd.CommandText = "UPDATE Pessoa SET Nome_Pes = @Nome_Pes WHERE Cpf = @Cpf";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Nome_Pes", novoNome));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Nome alterado com secesso!");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Write("Informe o novo sexo: ");
                    char novoSexo = char.Parse(Console.ReadLine().ToUpper());

                    cmd.CommandText = "UPDATE Pessoa SET Sexo = @Sexo WHERE Cpf = @Cpf";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Sexo", novoSexo));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Sexo alterado com secesso");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Write("Informe a nova data de nascimento (ddMMyyyy): ");
                    string novaDat = Console.ReadLine().Replace("/", "");

                    cmd.CommandText = "UPDATE Pessoa SET Data_Nascimento = @Data_Nascimento WHERE Cpf = @Cpf";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Data_Nascimento", novaDat));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Data de nascimento alterada com secesso!");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Write("Informe o novo telefone: ");
                    string novoTelefone = Console.ReadLine().Replace("(", "").Replace(")", "").Replace("-", "");

                    cmd.CommandText = "UPDATE Pessoa SET Telefone = @Telefone WHERE Cpf = @Cpf";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Telefone", novoTelefone));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Telefone alterado com secesso!");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Informe os dados do novo endereço");
                    Console.Write("Informe o logradouro da pessoa: ");
                    string novoLogradouro = Console.ReadLine().ToUpper();

                    Console.Write("Informe o numero da casa da pessoa: ");
                    int novoNumero = int.Parse(Console.ReadLine());

                    Console.Write("Informe o bairro da pessoa: ");
                    string novoBairro = Console.ReadLine().ToUpper();

                    Console.Write("Informe a cidade da pessoa: ");
                    string novaCidade = Console.ReadLine().ToUpper();

                    Console.Write("Informe o estado da pessoa: ");
                    string novoEstado = Console.ReadLine().ToUpper();

                    Console.Write("Informe o cep da pessoa: ");
                    string novoCep = Console.ReadLine().Replace("-", "");

                    cmd.CommandText = "UPDATE Pessoa SET Logradouro = @Logradouro, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Cep = @Cep WHERE Cpf = @Cpf";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Logradouro", novoLogradouro));
                    cmd.Parameters.Add(new SqlParameter("@Numero", novoNumero));
                    cmd.Parameters.Add(new SqlParameter("@Bairro", novoBairro));
                    cmd.Parameters.Add(new SqlParameter("@Cidade", novaCidade));
                    cmd.Parameters.Add(new SqlParameter("@Estado", novoEstado));
                    cmd.Parameters.Add(new SqlParameter("@Cep", novoCep));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Endereço alterado com secesso!");
                    Console.ReadKey();
                    break;

                default:
                    Console.Write("\n Opcao Inválida! Aperte [ENTER] para executar novamente.");
                    Console.ReadKey();
                    break;
            }
        }
        #endregion

        #region CRUD ANIMAL
        public void InserirAnimalBanco()
        {
            int count = 0;
            Animal animal = new Animal();

            animal.CadastraAnimal();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Animal(Familia, Raca, Sexo, Nome_Ani) VALUES (@Familia, @Raca, @Sexo, @Nome_Ani);";

            SqlParameter familia = new SqlParameter("@Familia", SqlDbType.VarChar, 50);
            SqlParameter raca = new SqlParameter("@Raca", SqlDbType.VarChar, 50);
            SqlParameter sexo = new SqlParameter("@Sexo", SqlDbType.Char, 1);
            SqlParameter nomeAni = new SqlParameter("@Nome_Ani", SqlDbType.VarChar, 50);

            familia.Value = animal.Familia;
            raca.Value = animal.Raca;
            sexo.Value = animal.Sexo;
            nomeAni.Value = animal.Nome;

            cmd.Parameters.Add(familia);
            cmd.Parameters.Add(raca);
            cmd.Parameters.Add(sexo);
            cmd.Parameters.Add(nomeAni);

            cmd.Connection = conexaosql;
            cmd.ExecuteNonQuery();

            conexaosql.Close();

            conexaosql.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Animal";

            cmd.Connection = conexaosql;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }

            cmd = new SqlCommand("INSERT INTO AnimaisDisponiveis VALUES (@Chip);", conexaosql);

            cmd.Parameters.Add(new SqlParameter("@Chip", count));

            cmd.ExecuteNonQuery();

            conexaosql.Close();

            Console.WriteLine("Cadastro concluído\n");
            Console.ReadKey();
        }

        public void SelecionarAnimaisBanco()
        {
            Animal animal = new Animal();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Chip, Familia, Raca, Sexo, Nome_Ani FROM Animal";

            cmd.Connection = conexaosql;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}", reader.GetInt32(0));
                    Console.WriteLine("{0}", reader.GetString(1));
                    Console.WriteLine("{0}", reader.GetString(2));
                    Console.WriteLine("{0}", reader.GetString(3));
                    Console.WriteLine("{0}", reader.GetString(4));
                    Console.WriteLine("\n----------------------------\n");
                }
            }
            conexaosql.Close();
            Console.ReadKey();
        }

        public void DeletarAnimalBanco()
        {
            Animal anima = new Animal();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM Animal where Chip = @Chip";
            cmd.Connection = conexaosql;

            Console.Write("Informe o CHIP do animal que deseja deletar: ");
            int chipRemovido = int.Parse(Console.ReadLine());

            cmd.Parameters.Add(new SqlParameter("@Chip", chipRemovido));

            cmd.ExecuteNonQuery();

            Console.WriteLine("Animal deletado com sucesso.");

            conexaosql.Close();
        }

        public void AtualizarAnimalBanco()
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand();
            Banco conn = new Banco();
            SqlConnection conexaoSql = new SqlConnection(conn.Caminho());

            Console.Write("Informe o CHIP que deseja atualizar");
            string chipDigitado = Console.ReadLine();

            conexaoSql.Open();

            cmd = new();
            
            cmd.CommandText = "SELECT * FROM Animal where Chip = @Chip";

            cmd.Connection = conexaoSql;

            cmd.Parameters.Add(new SqlParameter("@Chip", chipDigitado));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("CHIP digitado não localizado!");
                Console.ReadKey();
                return;
            }

            int opcao = 0;
            do
            {
                Console.Write("Informe a opcao que deseja alterar: ");
                Console.WriteLine(" 1 - Familia");
                Console.WriteLine(" 2 - Raça");
                Console.WriteLine(" 3 - Sexo");
                Console.WriteLine(" 4 - Nome do animal");
                Console.Write(" Informe a opcao: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                }

            } while (opcao < 1 || opcao > 4);

            switch (opcao)
            {
                case 1:
                    Console.Write("Informe a nova familia: ");
                    string novaFamilia = Console.ReadLine().ToUpper();

                    cmd.CommandText = "UPDATE Animal SET Familia = @Familia WHERE Chip = @Chip";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Familia", novaFamilia));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Familia alterada com secesso!");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Write("Informe a nova raça: ");
                    string novaRaca = Console.ReadLine().ToUpper();

                    cmd.CommandText = "UPDATE Animal SET Raca = @Raca WHERE Chip = @Chip";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Raca", novaRaca));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Raca alterado com secesso!");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Write("Informe o novo sexo: ");
                    char novoSexo = char.Parse(Console.ReadLine().ToUpper());

                    cmd.CommandText = "UPDATE Animal SET Sexo = @Sexo WHERE Chip = @Chip";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Sexo", novoSexo));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Sexo alterado com secesso!");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Write("Informe o novo nome: ");
                    string novoNome = Console.ReadLine().ToUpper();

                    cmd.CommandText = "UPDATE Animal SET Nome_Ani = @Nome_Ani WHERE Chip = @Chip";
                    cmd.Connection = conexaoSql;

                    cmd.Parameters.Add(new SqlParameter("@Nome_Ani", novoNome));

                    cmd.ExecuteNonQuery();
                    conexaoSql.Close();
                    Console.WriteLine("Nome alterado com secesso!");
                    Console.ReadKey();
                    break;

                default:
                    Console.Write("\n Opcao Inválida! Aperte [ENTER] para executar novamente.");
                    Console.ReadKey();
                    break;
            }
        }
        #endregion

        #region ADOÇÃO
        public void AdotarAnimal()
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand();

            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            Console.Write("informe o CPF da pessoa que ira adotar: ");
            string cpfAdotante = Console.ReadLine();

            cmd = new();

            cmd.CommandText = "SELECT * FROM Pessoa where Cpf = @Cpf";
            cmd.Connection = conexaosql;

            cmd.Parameters.Add(new SqlParameter("@Cpf", cpfAdotante));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("CPF digitado não cadastrado!");
                Console.ReadKey();
                conexaosql.Close();
                return;
            }
            conexaosql.Close();

            conexaosql.Open();
            count = 0;

            Console.Write("informe o CHIP do animal que ira ser adotado: ");
            string chipAdotado = Console.ReadLine();

            cmd = new();

            cmd.CommandText = "SELECT * FROM AnimaisDisponiveis where Chip = @Chip";
            cmd.Connection = conexaosql;

            cmd.Parameters.Add(new SqlParameter("@Chip", chipAdotado));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("CHIP digitado não cadastrado!");
                Console.ReadKey();
                conexaosql.Close();
                return;
            }
            conexaosql.Close();

            conexaosql.Open();

            cmd = new();

            cmd.CommandText = "INSERT INTO Adocao(Cpf, Chip) VALUES (@Cpf, @Chip);";
            cmd.Connection = conexaosql;

            cmd.Parameters.Add(new SqlParameter("@Cpf", cpfAdotante));
            cmd.Parameters.Add(new SqlParameter("@Chip", chipAdotado));

            cmd.ExecuteNonQuery();

            cmd = new("DELETE FROM AnimaisDisponiveis where Chip = @Chip", conexaosql);

            cmd.Parameters.Add(new SqlParameter("@Chip", chipAdotado));

            cmd.ExecuteNonQuery();

            conexaosql.Close();

            Console.WriteLine("Adoção concluida!\n");
            Console.ReadKey();
        }
        #endregion
    }
}