using System;

class FilaHospital
{
    static Paciente[] fila = new Paciente[11];  
    static int inicio = 0;
    static int fim = 0;
    static int numeroSenha = 1;  

    class Paciente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public int Senha { get; set; }

        public Paciente(string nome, string cpf, string telefone, int idade, int senha)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Idade = idade;
            Senha = senha;
        }
    }

    static void Main(string[] args)
    {
        char opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("Sistema de Controle de Fila de Pacientes");
            Console.WriteLine("1. Cadastrar paciente e incluir na fila");
            Console.WriteLine("2. Incluir paciente prioritário (por idade)");
            Console.WriteLine("3. Listar pacientes da fila");
            Console.WriteLine("4. Atender paciente");
            Console.WriteLine("5. Verificar sua senha e posição na fila");
            Console.WriteLine("q. Sair");
            Console.Write("Escolha uma opção: ");
            opcao = Console.ReadKey().KeyChar;

            switch (opcao)
            {
                case '1':
                    InserirPaciente();
                    break;
                case '2':
                    InserirPacientePrioritario();
                    break;
                case '3':
                    ListarPacientes();
                    break;
                case '4':
                    AtenderPaciente();
                    break;
                case '5':
                    VerificarSenhaPosicao();
                    break;
                case 'q':
                    Console.WriteLine("\nSaindo do sistema...");
                    break;
                default:
                    Console.WriteLine("\nOpção inválida! Tente novamente.");
                    break;
            }

            if (opcao != 'q')
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 'q');
    }

    static Paciente CadastrarPaciente()
    {
        Console.Write("\nDigite o nome do paciente: ");
        string nome = Console.ReadLine();
        Console.Write("Digite o CPF do paciente: ");
        string cpf = Console.ReadLine();
        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine();
        Console.Write("Digite a idade do paciente: ");
        int idade = int.Parse(Console.ReadLine());

        Paciente paciente = new Paciente(nome, cpf, telefone, idade, numeroSenha++);
        Console.WriteLine($"\nPaciente {nome} cadastrado com sucesso! Sua senha é {paciente.Senha}");
        return paciente;
    }

    static void InserirPaciente()
    {
        if ((fim + 1) % fila.Length == inicio)
        {
            Console.WriteLine("\nA fila está cheia!");
            return;
        }

        Paciente paciente = CadastrarPaciente();

        fila[fim] = paciente;
        fim = (fim + 1) % fila.Length;

        Console.WriteLine($"\nPaciente {paciente.Nome} inserido na fila com sucesso! Sua senha é {paciente.Senha}");
    }

    static void InserirPacientePrioritario()
    {
        if ((fim + 1) % fila.Length == inicio)
        {
            Console.WriteLine("\nA fila está cheia!");
            return;
        }

        Paciente paciente = CadastrarPaciente();

        if (paciente.Idade < 60)
        {
            Console.WriteLine("\nA prioridade é dada apenas para pacientes com 60 anos ou mais.");
            InserirPaciente();
            return;
        }

       
        for (int i = fim; i != inicio; i = (i - 1 + fila.Length) % fila.Length)
        {
            fila[i] = fila[(i - 1 + fila.Length) % fila.Length];
        }

        fila[inicio] = paciente;
        fim = (fim + 1) % fila.Length;

        Console.WriteLine($"\nPaciente prioritário {paciente.Nome} inserido como próximo a ser atendido!");
    }

    static void ListarPacientes()
    {
        if (inicio == fim)
        {
            Console.WriteLine("\nA fila está vazia!");
            return;
        }

        Console.WriteLine("\nPacientes na fila:");

        for (int i = inicio; i != fim; i = (i + 1) % fila.Length)
        {
            Paciente p = fila[i];
            Console.WriteLine($"{(i - inicio + fila.Length) % fila.Length + 1}. Nome: {p.Nome}, Idade: {p.Idade}, Senha: {p.Senha}");
        }
    }

    static void AtenderPaciente()
    {
        if (inicio == fim)
        {
            Console.WriteLine("\nNão há pacientes na fila para serem atendidos!");
            return;
        }

        Paciente paciente = fila[inicio];
        Console.WriteLine($"\nPaciente {paciente.Nome} com a senha {paciente.Senha} foi atendido.");
        fila[inicio] = null;  
        inicio = (inicio + 1) % fila.Length;
    }

    static void VerificarSenhaPosicao()
    {
        Console.Write("\nDigite sua senha: ");
        int senha = int.Parse(Console.ReadLine());

        for (int i = inicio; i != fim; i = (i + 1) % fila.Length)
        {
            if (fila[i] != null && fila[i].Senha == senha)
            {
                Console.WriteLine($"\nPaciente {fila[i].Nome}, sua posição na fila é {(i - inicio + fila.Length) % fila.Length + 1}.");
                return;
            }
        }

        Console.WriteLine("\nSenha não encontrada.");
    }
}

