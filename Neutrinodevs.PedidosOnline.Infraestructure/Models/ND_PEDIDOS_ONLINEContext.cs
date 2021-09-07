using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class ND_PEDIDOS_ONLINEContext : DbContext
    {
        public ND_PEDIDOS_ONLINEContext()
        {
        }

        public ND_PEDIDOS_ONLINEContext(DbContextOptions<ND_PEDIDOS_ONLINEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<ComprobanteDetalle> ComprobanteDetalle { get; set; }
        public virtual DbSet<ComprobanteVenta> ComprobanteVenta { get; set; }
        public virtual DbSet<Mesas> Mesas { get; set; }
        public virtual DbSet<PedidoDetalle> PedidoDetalle { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=BDUARTE-LAP; Initial Catalog=ND_PEDIDOS_ONLINE; user id=sa; password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("CLIENTES");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasColumnName("identificacion")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCliente).HasColumnName("tipo_cliente");
            });

            modelBuilder.Entity<ComprobanteDetalle>(entity =>
            {
                entity.HasKey(e => e.IdComprobanteDet);

                entity.ToTable("COMPROBANTE_DETALLE");

                entity.Property(e => e.IdComprobanteDet).HasColumnName("id_comprobante_det");

                entity.Property(e => e.ComprobanteId).HasColumnName("comprobante_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.HasOne(d => d.Comprobante)
                    .WithMany(p => p.ComprobanteDetalle)
                    .HasForeignKey(d => d.ComprobanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPROBANTE_DETALLE_COMPROBANTE_DETALLE");
            });

            modelBuilder.Entity<ComprobanteVenta>(entity =>
            {
                entity.HasKey(e => e.IdComprobante);

                entity.ToTable("COMPROBANTE_VENTA");

                entity.Property(e => e.IdComprobante).HasColumnName("id_comprobante");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("iva")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<Mesas>(entity =>
            {
                entity.HasKey(e => e.IdMesa);

                entity.ToTable("MESAS");

                entity.Property(e => e.IdMesa).HasColumnName("id_mesa");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PedidoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdPedidoDet);

                entity.ToTable("PEDIDO_DETALLE");

                entity.Property(e => e.IdPedidoDet)
                    .HasColumnName("id_pedido_det")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasColumnName("nombre_producto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PedidoId).HasColumnName("pedido_id");

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.PedidoDetalle)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDO_DETALLE_PEDIDOS");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.PedidoDetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDO_DETALLE_PRODUCTOS");
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.ToTable("PEDIDOS");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDOS_PEDIDOS");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("PRODUCTOS");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombres)
                    .HasColumnName("nombres")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");
            });
        }
    }
}
