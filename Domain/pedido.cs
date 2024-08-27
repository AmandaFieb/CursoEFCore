using CursoEFCore.ValueObjects;
using System;

namespace CursoEFCore.Domain
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; } = null!;

        public DateTime IniciadoEm { get; set; }

        public DateTime FinalizadoEm { get; set; }

        public TipoFrete TipoFrete { get; set; }

        public StatusPedido Status { get; set; }

        public string Observacao { get; set; } = string.Empty;

        public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    }
}
