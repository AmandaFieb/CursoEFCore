using System;
using System.Linq;
using System.Collections.Generic;
using CursoEFCore.Data;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using CursoEFCore.ValueObjects;

namespace CursoEFCore
{
    class Program
    {

        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            //db.Database.Migrate();
            //var existe = db.Database.GetPendingMigrations().Any();
            //if(existe)
            //{

            //}

            //InserirDados();

            //InserirDadosEmMassa();

            //ConsultarDados();

            //CadastrarPedido();

            //ConsultarPedidoCarregamentoAdiantado();

            //AtualizarDados();

            RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            db.Entry(cliente).State = EntityState.Deleted;
            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Name = "Cliente Desconectado Passo 3",
                Telefone = "9823738631"

            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            db.SaveChanges();

        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p=>p.Itens)
                    .ThenInclude(p=>p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        


        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();

            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido

            {

                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemiFrete,
                Itens = new List<PedidoItem>
                    {  
                          new PedidoItem
                           {
                             ProdutoId = produto.Id,
                             Desconto = 0,
                             Quantidade = 1,
                             Valor = 10,
                           }
                    }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                
                .Where(p=>p.Id >0)
                .OrderBy(p => p.Id)
                .ToList();
           
            foreach(var cliente  in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p=>p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true,
            };

            var cliente = new Cliente
            {
                Nome = "Amanda Novais",
                CEP = "06414007",
                Cidade = "Barueri",
                Estado = "SP",
                Telefone = "11965765213"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true,
            };

            using var db = new Data.ApplicationContext();
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}
