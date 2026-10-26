// EUREKA!!!!! 100% FUNCIONAL!


using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Produto> listaDeProdutos = new List<Produto>();
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE PRODUTOS COM DESCONTO ===");
            Console.WriteLine("1. Cadastrar Novo Produto");
            Console.WriteLine("2. Listar Todos os Produtos");
            Console.WriteLine("3. Editar Desconto de um Produto");
            Console.WriteLine("4. Adicionar / Remover Estoque");
            Console.WriteLine("5. Sair");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarProduto(listaDeProdutos);
                    break;
                case "2":
                    ListarProdutos(listaDeProdutos);
                    break;
                case "3":
                    EditarDesconto(listaDeProdutos);
                    break;
                case "4":
                    GerenciarEstoque(listaDeProdutos);
                    break;
                case "5":
                    executando = false;
                    break;
                default:
                    Console.WriteLine("\nOpção inválida. Aperte ENTER.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void CadastrarProduto(List<Produto> produtos)
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRO DE PRODUTO ---");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Preço R$: ");
        double preco = double.Parse(Console.ReadLine());

        Console.Write("Quantidade Inicial: ");
        int quantidade = int.Parse(Console.ReadLine());

        Console.Write("Desconto Inicial (%): ");
        double desconto = double.Parse(Console.ReadLine());

        produtos.Add(new Produto(nome, preco, quantidade, desconto));

        Console.WriteLine("\nProduto cadastrado com sucesso! Aperte ENTER.");
        Console.ReadLine();
    }

    static void ListarProdutos(List<Produto> produtos)
    {
        Console.Clear();
        Console.WriteLine("--- LISTA DE PRODUTOS ---");

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
        }
        else
        {
            double valorTotalGeral = 0;

            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {produtos[i]}");
                valorTotalGeral += produtos[i].ValorTotalEmEstoque();
            }

            Console.WriteLine($"\nValor Total em Estoque (com descontos): {valorTotalGeral:C}");
        }

        Console.WriteLine("\nAperte ENTER para voltar.");
        Console.ReadLine();
    }

    static void EditarDesconto(List<Produto> produtos)
    {
        Console.Clear();
        Console.WriteLine("--- EDITAR DESCONTO ---");

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto para editar.");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {produtos[i].Nome} - Desconto Atual: {produtos[i].DescontoPercentual}%");
        }

        Console.Write("\nEscolha o número do produto: ");
        int indice = int.Parse(Console.ReadLine()) - 1;

        if (indice >= 0 && indice < produtos.Count)
        {
            Console.Write($"Digite o novo desconto (%) para '{produtos[indice].Nome}': ");
            double novoDesconto = double.Parse(Console.ReadLine());

            produtos[indice].DescontoPercentual = novoDesconto;

            Console.WriteLine("\nDesconto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("\nProduto inválido.");
        }

        Console.ReadLine();
    }

    static void GerenciarEstoque(List<Produto> produtos)
    {
        Console.Clear();
        Console.WriteLine("--- GERENCIAR ESTOQUE ---");

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {produtos[i].Nome} (Qtd: {produtos[i].Quantidade})");
        }

        Console.Write("\nEscolha o número do produto: ");
        int indice = int.Parse(Console.ReadLine()) - 1;

        if (indice >= 0 && indice < produtos.Count)
        {
            Console.Write("Digite a quantidade (use valor positivo para somar, negativo para subtrair): ");
            int qtd = int.Parse(Console.ReadLine());

            if (qtd >= 0)
                produtos[indice].AdicionarEstoque(qtd);
            else
                produtos[indice].RemoverEstoque(Math.Abs(qtd));

            Console.WriteLine("\nEstoque alterado com sucesso!");
        }

        Console.ReadLine();
    }
}


class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public double DescontoPercentual { get; set; }

    public Produto(string nome, double preco, int quantidade, double descontoPercentual = 0)
    {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        DescontoPercentual = descontoPercentual;
    }

    public double PrecoComDesconto()
    {
        return Preco * (1 - (DescontoPercentual / 100.0));
    }

    public double ValorTotalEmEstoque()
    {
        return PrecoComDesconto() * Quantidade;
    }

    public void AdicionarEstoque(int quantidade)
    {
        Quantidade += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        if (quantidade <= Quantidade)
        {
            Quantidade -= quantidade;
        }
        else
        {
            Console.WriteLine("Quantidade insuficiente em estoque!");
        }
    }

    public override string ToString()
    {
        return $"{Nome} | Preço Orig: {Preco:C} | Desc: {DescontoPercentual}% | Preço c/ Desc: {PrecoComDesconto():C} | Qtd: {Quantidade} | Total: {ValorTotalEmEstoque():C}";
    }
}
